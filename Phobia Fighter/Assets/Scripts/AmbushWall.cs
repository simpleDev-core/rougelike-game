using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Reflection;
[RequireComponent(typeof(UnityEngine.Rendering.Universal.ShadowCaster2D))]
[DefaultExecutionOrder(100)]
public class AmbushWall : MonoBehaviour
{
    public Sprite trapSprite;
    UnityEngine.Rendering.Universal.ShadowCaster2D shadow;
    // Start is called before the first frame update
    void Start()
    {
        shadow = GetComponent<UnityEngine.Rendering.Universal.ShadowCaster2D>();
        shadow.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Ambush()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = trapSprite;
        shadow.enabled = true;

    }
}
