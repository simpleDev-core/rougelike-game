using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject player;
    Rigidbody2D rb;
    public float speed;
    public float maxSpeed;
    Vector2 CurrentSpeed;
    Vector2 OldSpeed;
    bool walk = true;
    float dragFactor = 0.75f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (walk)
        {
            rb.velocity += new Vector2(((player.transform.position - gameObject.transform.position).normalized * speed).x, ((player.transform.position - gameObject.transform.position).normalized * speed).y);
        }
        rb.velocity *= dragFactor;
    }

    public void Stun()
    {
        StartCoroutine(stun());
    }

    IEnumerator stun()
    {
        dragFactor = 0.99f;
        walk = false;
        yield return new WaitForSeconds(0.5f);
        walk = true;
        dragFactor = 0.75f;
        yield break;
    }
}
