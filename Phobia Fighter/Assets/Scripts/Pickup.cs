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
                swordManager.sword = sword[ index];
            }
            else
            {
                if (!(playerMovement.talisman1 != null))
                {
                    playerMovement.talisman1 = talisman[index];
                }
                else
                {
                    playerMovement.talisman2 = talisman[index];
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
