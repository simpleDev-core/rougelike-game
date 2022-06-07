using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoTowards : MonoBehaviour
{
    public Transform target;
    public float speed;
    Rigidbody2D rb;
    public bool trackPlayer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (trackPlayer)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 veloc = (target.position - gameObject.transform.position) * speed;
        rb.velocity += veloc;
    }
}
