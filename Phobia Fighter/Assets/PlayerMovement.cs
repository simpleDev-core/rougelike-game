using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;
    public float dashDelay = 1;
    bool Dash;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    IEnumerator DashDelay()
    {
        Dash = true;
        yield return new WaitForSeconds(dashDelay);
        print("DashFalse");
        Dash = false;
        yield break;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity += Vector2.up * speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.velocity += Vector2.down * speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity += Vector2.left * speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity += Vector2.right * speed;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(DashDelay());
            rb.velocity *= 10;
        }
        if (Dash)
        {
            rb.velocity *= 0.9f;
        }
        else
        {
            rb.velocity *= 0.7f;
        }
    }
}
