using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visibility : MonoBehaviour
{
    public bool visible;
    public GameObject root;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (visible)
        {
            if (!root.activeInHierarchy)
            {
                root.SetActive(true);
            }
        }
        else
        {
            if (root.activeInHierarchy)
            {
                root.SetActive(false);
            }
        }
    }
    private void OnBecameVisible()
    {
        visible = true;
    }
    private void OnBecameInvisible()
    {
        visible = false;
    }
}
