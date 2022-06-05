using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollCamZoom : MonoBehaviour
{
    public Camera camZoom;
    public bool mapOpen = true;
    Camera main;
    // Start is called before the first frame update
    void Start()
    {
        main = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (!mapOpen)
        {
            if(camZoom.orthographicSize + Input.mouseScrollDelta.y > 0)
            {
                camZoom.orthographicSize += Input.mouseScrollDelta.y;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                //camZoom.rect = new Rect(camZoom.rect);
                print(camZoom.rect.position); //= Time.deltaTime;
            }
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (mapOpen)
            {
                GetComponent<Camera>().targetDisplay = 0;
                main.targetDisplay = 1;
            }
            else
            {
                
                GetComponent<Camera>().targetDisplay = 1;
                main.targetDisplay = 0;
            }
            mapOpen = !mapOpen;
        }
    }
}
