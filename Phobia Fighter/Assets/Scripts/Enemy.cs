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
    public enum attackType {Lunge, Projectile, Contact, SpinLaser}
    public attackType Attack;
    public float moveRange = 0.1f;
    bool walk = true;
    
    float dragFactor = 0.75f;
    public float VisionRange = 10;
    public int trackDestination = -1;
    public float attackCooldown = 1;
    public float attackWindup = 0.5f;
    bool cooldownAttack;
    List<Vector2> visibleTracks = new List<Vector2>();
    bool locked;
    bool tracking;
    bool following;
    public float stuckThreshold;
    int stuckCounter;
    public bool debug;
    List<Vector2> TrackedPositions = new List<Vector2>();
    public LayerMask IgnoreMe;
    LineRenderer laserLine;
    public float laserOffset = 0;
    DamageManager damageManager;
    // Start is called before the first frame update
    void Start()
    {
        damageManager = GetComponent<DamageManager>();
        if(Attack == attackType.SpinLaser)
        {
            laserLine = GetComponent<LineRenderer>();
        }
        target = gameObject.transform.position;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        gameObject.transform.parent = null;
        gameObject.transform.rotation = Quaternion.FromToRotation(transform.up, Vector2.up);
        
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

    IEnumerator Lunge(Vector2 lunge, float windup)
    {
        StartCoroutine(stun(windup+1));
        yield return new WaitForSeconds(windup);
        rb.velocity += lunge;
        yield break;

    }

    IEnumerator AttackCooldown(float time)
    {
        cooldownAttack = true;
        yield return new WaitForSeconds(time);
        cooldownAttack = false;
    }
    // Update is called once per frame
    void Update()
    {





        if (Attack == attackType.SpinLaser && Vector2.Distance(player.transform.position, transform.position) <= 16)
        {
            RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, new Vector2(Mathf.Cos(Time.time + laserOffset), Mathf.Sin(Time.time + laserOffset)),Mathf.Infinity, layerMask:~IgnoreMe);
            if(hit.collider != null)
            {
                Debug.DrawLine(transform.position, hit.point);
                laserLine.SetPosition(0, transform.position);
                laserLine.SetPosition(1, hit.point);
                if(hit.collider.gameObject.GetComponent<healthManager>() != null)
                {
                    hit.collider.gameObject.GetComponent<healthManager>().Damage(10*Time.deltaTime);
                }
            }
            else
            {
                Debug.DrawLine(transform.position, new Vector2(Mathf.Cos(Time.time+laserOffset), Mathf.Sin(Time.time + laserOffset))*100);
                laserLine.SetPosition(0, transform.position);
                laserLine.SetPosition(1, new Vector3(Mathf.Cos(Time.time + laserOffset), Mathf.Sin(Time.time + laserOffset),0)*100+gameObject.transform.position);
            }
        }
        if (walk)
        {
            
            if (Vector2.Distance(player.transform.position, gameObject.transform.position) <= moveRange && !cooldownAttack)
            {
                StartCoroutine(AttackCooldown(attackCooldown));
                if (Attack == attackType.Lunge)
                {
                    StartCoroutine(Lunge((player.transform.position-transform.position)*10,attackCooldown));
                }
                if (Attack == attackType.Projectile)
                {
                    StartCoroutine(stun(1));
                }
                
            }












            rb.velocity *= dragFactor;
            RaycastHit2D hit= Physics2D.Raycast(gameObject.transform.position, (player.transform.position-gameObject.transform.position), Mathf.Infinity, ~IgnoreMe);
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
                        }

                    }
                    else
                    {
                        if (Vector2.Distance(transform.position, target) <= moveRange)
                        {
                            //print("nextDestination");
                            if (trackDestination < TrackedPositions.Count)
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
                        if (trackDestination < TrackedPositions.Count)
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
                //print(hit.collider.gameObject.name);
                
                //print(Vector2.Distance(transform.position, player.transform.position));
            }
            
        }
        
    }

    public void Stun()
    {
        StartCoroutine(stun(0.75f));
    }

    IEnumerator stun(float time)
    {
        dragFactor = 0.99f;
        walk = false;
        yield return new WaitForSeconds(time);
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
                    Gizmos.color = new Color(i / TrackedPositions.Count, i / TrackedPositions.Count, i / TrackedPositions.Count);
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
            Gizmos.DrawLine(transform.position, transform.position + new Vector3(Mathf.Cos(Time.time + laserOffset), Mathf.Sin(Time.time + laserOffset),0) * 100);
        }
        
    }
}
