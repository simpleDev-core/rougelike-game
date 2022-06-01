using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public SwordObject[] sword;
    public Talisman[] talisman;
    SwordManager swordManager;
    PlayerMovement playerMovement;
    AudioSource source;
    bool trigger;
    int index;
    bool talismanActive;
    // Start is called before the first frame update
    void Start()
    {
        if (Random.Range(0, 5) < 4)
        {
            talismanActive = true;
            index = Mathf.RoundToInt(Random.Range(0, talisman.Length));
            GetComponent<SpriteRenderer>().sprite = talisman[index].icon;
        }
        else
        {
            talismanActive = false;
            index = Mathf.RoundToInt(Random.Range(0, sword.Length));
            GetComponent<SpriteRenderer>().sprite = sword[index].swordImage;
        }
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
            if (!talismanActive)
            {
                
                
                if (swordManager.sword != null)
                {
                    talismanActive = false;
                    
                    SwordObject temp = swordManager.sword;
                    
                    swordManager.sword = sword[index];
                    sword.SetValue(temp, index);
                    GetComponent<SpriteRenderer>().sprite = sword[index].swordImage;
                }
                else
                {
                    swordManager.sword = sword[index];
                    Destroy(gameObject);
                }
            }
            else
            {
                if (!(playerMovement.talisman1 != null))
                {
                    playerMovement.talisman1 = talisman[index];
                }
                else
                {
                    if(playerMovement.talisman2 != null)
                    {
                        playerMovement.talisman2 = talisman[index];
                        Talisman temp = playerMovement.talisman2;
                        talisman.SetValue(temp, index);
                        GetComponent<SpriteRenderer>().sprite = talisman[index].icon;
                        talismanActive = true;
                    }
                    else
                    {
                        playerMovement.talisman2 = talisman[index];
                        Destroy(gameObject);
                    }
                    
                }
            }
            if (GetComponent<AudioSource>() != null)
            {
                GetComponent<AudioSource>().Play();
            }
        }

    }
}
