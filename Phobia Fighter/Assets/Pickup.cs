using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public SwordObject sword;
    public Talisman talisman;
    SwordManager swordManager;
    PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        swordManager = GameObject.FindGameObjectWithTag("Sword").GetComponent<SwordManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(sword != null)
        {
            swordManager.sword = sword;
        }
        if (talisman != null)
        {
            if(!(playerMovement.talisman1 != null))
            {
                playerMovement.talisman1 = talisman;
            }
            else
            {
                playerMovement.talisman2 = talisman;
            }
        }
        Destroy(gameObject);
    }
}