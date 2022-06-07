using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    public bool showMiniMaps;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (showMiniMaps)
        {
            showMiniMaps = false;
            foreach(MinimapRoomReveal mini in FindObjectsOfType<MinimapRoomReveal>())
            {
                mini.setRenderers(true);
                mini.enabled = false;
            }
            showMiniMaps = false;
        }
    }
    
}
