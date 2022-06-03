using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapRoomReveal : MonoBehaviour
{
    public bool startShown;
    public Vector2 bounds;
    public GameObject[] Objects;
    public int frameCheck = 5;
    // Start is called before the first frame update
    void Start()
    {
        if (startShown)
        {
            setRenderers(true);
        }
        else
        {
            setRenderers(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.frameCount % frameCheck == 0)
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(gameObject.transform.position, bounds, 0);
            foreach(Collider2D collider in colliders)
            {
                if(collider.gameObject.tag == "Player")
                {
                    setRenderers(true);
                    gameObject.GetComponent<MinimapRoomReveal>().enabled = false;
                }
            }
        }
    }

    public void setRenderers(bool enabled)
    {
        foreach(GameObject Object in Objects)
        {
            Object.SetActive(enabled);
        }
    }
    
}
