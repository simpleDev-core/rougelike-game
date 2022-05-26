using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
[RequireComponent(typeof(Sprite))]
[RequireComponent(typeof(UnityEngine.Rendering.Universal.Light2D))]
public class SyncLightWithSprite : MonoBehaviour
{
    public UnityEngine.Rendering.Universal.Light2D _light2D;
    public SpriteRenderer sprite;

    //private UnityEngine.Rendering.Universal.Light2D _light2D;
    private FieldInfo _LightCookieSprite = typeof(UnityEngine.Rendering.Universal.Light2D).GetField("m_LightCookieSprite", BindingFlags.NonPublic | BindingFlags.Instance);


    void UpdateCookieSprite(Sprite sprite)
    {
        _LightCookieSprite.SetValue(_light2D, sprite);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        UpdateCookieSprite(sprite.sprite);
    }
}
