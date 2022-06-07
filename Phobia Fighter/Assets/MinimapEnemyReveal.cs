using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapEnemyReveal : MonoBehaviour
{
    public float range = 16;
    public new SpriteRenderer renderer;
    public LayerMask Ignore;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        print(player.name);
        renderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit;
        if(Vector2.Distance(gameObject.transform.position,player.transform.position)<= range)
        {
            print("inrange");
            Debug.DrawRay(gameObject.transform.position, ( player.transform.position-gameObject.transform.position)*5,Color.yellow);
            hit = Physics2D.Raycast(gameObject.transform.position, player.transform.position - gameObject.transform.position, range, layerMask:~Ignore);
            if (hit.point != null)
            {
                print("hit");
                if(hit.collider.gameObject.tag == "Player")
                {
                    renderer.enabled = true;
                    gameObject.GetComponent<MinimapEnemyReveal>().enabled = false;
                }
            }
        }
        
    }
}
