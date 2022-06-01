using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]
public class ShowInventorySprite : MonoBehaviour
{
    public enum showType { sword, talisman1, talisman2 }
    Image sprite;
    public showType type;
    PlayerMovement playerScript;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<Image>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

    }

    // Update is called once per frame
    void Update()
    {
        if (type == showType.sword)
        {
            sprite.sprite = GameObject.FindGameObjectWithTag("Sword").GetComponent<SwordManager>().sword.swordImage;
        }
        else if (type == showType.talisman1)
        {
            sprite.sprite = playerScript.talisman1.icon;
        }
        else if (type == showType.talisman1)
        {
            sprite.sprite = playerScript.talisman2.icon;
        }
    }

    public void drop()
    {
        if(type == showType.talisman1)
        {
            playerScript.talisman1 = null;
        }
        if (type == showType.talisman2)
        {
            playerScript.talisman2 = null;
        }
    }
}
