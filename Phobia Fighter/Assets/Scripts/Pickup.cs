using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public SwordObject sword;
    public Talisman talisman;
    SwordManager swordManager;
    PlayerMovement playerMovement;
    AudioSource source;
    bool trigger;
    // Start is called before the first frame update
    void Start()
    {
        
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        swordManager = GameObject.FindGameObjectWithTag("Sword").GetComponent<SwordManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PickupItem();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            trigger = true;
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            trigger = false;
        }
    }
    public void PickupItem()
    {
        if (trigger)
        {
            if (sword != null)
            {
                swordManager.sword = sword;
            }
            if (talisman != null)
            {
                if (!(playerMovement.talisman1 != null))
                {
                    playerMovement.talisman1 = talisman;
                }
                else
                {
                    playerMovement.talisman2 = talisman;
                }
            }
            if (GetComponent<AudioSource>() != null)
            {
                GetComponent<AudioSource>().Play();
            }
            Destroy(gameObject);
        }
    }
}
