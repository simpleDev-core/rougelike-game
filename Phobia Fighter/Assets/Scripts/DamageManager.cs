using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    public float damage = 1;
    public bool continuous = false;
    public GameObject instigator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<healthManager>() != null)
        {
            collision.gameObject.GetComponent<healthManager>().Damage(damage, instigator);

        }
    }
}
