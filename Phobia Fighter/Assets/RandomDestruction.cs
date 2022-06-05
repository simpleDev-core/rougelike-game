using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDestruction : MonoBehaviour
{
    public float chance;
    // Start is called before the first frame update
    void Start()
    {
        if (Random.Range(0, 100) < chance)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
