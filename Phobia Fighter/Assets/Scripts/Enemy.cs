using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject player;
    Rigidbody2D rb;
    public float speed;
    public float maxSpeed;
    Vector2 CurrentSpeed;
    Vector2 OldSpeed;
    Vector2 target;
    bool walk = true;
    float dragFactor = 0.75f;
    public float VisionRange = 10;
    public int trackDestination = -1;
    List<Vector2> visibleTracks = new List<Vector2>();
    bool locked;
    bool tracking;
    bool following;
    public float stuckThreshold;
    int stuckCounter;
    public bool debug;
    List<Vector2> TrackedPositions = new List<Vector2>();
    // Start is called before the first frame update
    void Start()
    {
        target = gameObject.transform.position;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    IEnumerator CollectTrackers()
    {
        tracking = true;
        while (locked)
        {
            TrackedPositions.Add(player.transform.position);
            if(TrackedPositions.Count >= 7)
            {
                TrackedPositions.RemoveAt(0);
            }
            yield return new WaitForSeconds(0.5f);
        }
        for(int x = 0; x <= 5; x++)
        {
            TrackedPositions.Add(player.transform.position);
            yield return new WaitForSeconds(0.5f);
        }
        tracking = false;
        yield break;
    }


    // Update is called once per frame
    void Update()
    {
        if (walk)
        {
            rb.velocity *= dragFactor;
            RaycastHit2D hit= Physics2D.Raycast(gameObject.transform.position, (player.transform.position-gameObject.transform.position));
            if (hit.collider.gameObject.tag == "Player" && Vector2.Distance(transform.position,player.transform.position)<= VisionRange)
            {
                if (!tracking)
                {
                    StartCoroutine(CollectTrackers());
                }
                //TrackedPositions = new List<Vector2>();
                trackDestination = -1;
                visibleTracks = new List<Vector2>();
                Debug.DrawLine(gameObject.transform.position, hit.point, Color.green);
                locked = true;
                rb.velocity += new Vector2(((player.transform.position - gameObject.transform.position).normalized * speed).x, ((player.transform.position - gameObject.transform.position).normalized * speed).y);
                
                
            }
            else
            {
                if (locked)
                {
                    locked = false;
                    following = true;
                }
                
                if (following)
                {
                    if (trackDestination == -1)
                    {
                        int index = 0;
                        foreach (Vector2 position in TrackedPositions)
                        {
                            index++;

                            if (!Physics2D.Linecast(gameObject.transform.position, position))
                            {
                                visibleTracks.Add(position);
                                //if (index > trackDestination)
                                if(Vector2.Distance(transform.position, position) < Vector2.Distance(transform.position,target))
                                {
                                    target = position;
                                    trackDestination = index;
                                }
                            }
                            if (debug)
                            {
                                Debug.Log(Physics2D.Linecast(gameObject.transform.position, position).collider.gameObject.name);
                                Debug.DrawLine(gameObject.transform.position, Physics2D.Linecast(gameObject.transform.position, position).point);
                            }
                            
                        }

                    }
                    else
                    {
                        if (Vector2.Distance(transform.position, target) <= 1)
                        {
                            print("nextDestination");
                            if (trackDestination < TrackedPositions.Count-1)
                            {
                                trackDestination += 1;
                                target = TrackedPositions[trackDestination];
                            }
                            else
                            {
                                target = gameObject.transform.position;
                                trackDestination = -1;
                                following = false;
                            }
                        }
                        
                    }
                    
                    rb.velocity += new Vector2(((new Vector3(target.x, target.y,0) - gameObject.transform.position).normalized * speed).x, ((new Vector3(target.x, target.y, 0) - gameObject.transform.position).normalized * speed).y);
                    if(rb.velocity.magnitude < stuckThreshold && following)
                    {
                        if (trackDestination < TrackedPositions.Count-1)
                        {
                            trackDestination += 1;
                            target = TrackedPositions[trackDestination];
                        }
                        else
                        {

                            if(trackDestination > 0)
                            {
                                trackDestination -= 1;
                                target = TrackedPositions[trackDestination];
                            }
                            else
                            {
                                target = gameObject.transform.position;
                                trackDestination = -1;
                                following = false;
                            }
                            
                        }
                        /*stuckCounter++;
                        if(stuckCounter >= 20)
                        {
                            stuckCounter = 0;
                            TrackedPositions.Remove(target);
                            if(TrackedPositions.Count >= 1)
                            {
                                if(TrackedPositions[trackDestination] != null)
                                {
                                    target = TrackedPositions[trackDestination];
                                    
                                }
                                else
                                {
                                    trackDestination = 0;
                                    target = TrackedPositions[trackDestination];
                                }
                            }
                            else
                            {
                                following = false;
                            }
                        }
                        else
                        {
                            print("stuck");
                        }*/
                    }
                }
                


                Debug.DrawLine(gameObject.transform.position, hit.point, Color.red);
                print(hit.collider.gameObject.name);
                
                print(Vector2.Distance(transform.position, player.transform.position));
            }
            
        }
        
    }

    public void Stun()
    {
        StartCoroutine(stun());
    }

    IEnumerator stun()
    {
        dragFactor = 0.99f;
        walk = false;
        yield return new WaitForSeconds(0.5f);
        walk = true;
        dragFactor = 0.75f;
        yield break;
    }
    private void OnDrawGizmos()
    {
        if (debug)
        {
            Gizmos.color = Color.white;
            for (int i = 0; i < TrackedPositions.Count; i++)
            {
                if (i < TrackedPositions.Count - 1)
                {
                    Gizmos.color = new Color(i / TrackedPositions.Count *255, i / TrackedPositions.Count * 255, i / TrackedPositions.Count * 255);
                    Gizmos.DrawLine(TrackedPositions[i], TrackedPositions[i + 1]);
                }
            }
            Gizmos.color = Color.yellow;
            foreach (Vector2 index in visibleTracks)
            {
                Gizmos.DrawWireSphere(index, 1);
            }
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(target, 1);
        }
        
    }
}
