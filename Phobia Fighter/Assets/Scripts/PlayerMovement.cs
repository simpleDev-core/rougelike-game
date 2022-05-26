using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;
    public float dashDelay = 1;
    bool Dash;
    bool cooldown;
    public Talisman talisman1;
    public float iFrameDuration = 1;
    public float dashCooldown = 1;
    public Talisman talisman2;
    public GameObject inventoryCanvas;
    healthManager damageManager;
    bool inventory;
    LineRenderer aimLine;
    public LayerMask ignore;
    public AudioManager audioManager;
    
    // Start is called before the first frame update
    void Start()
    {
        damageManager = GetComponent<healthManager>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    IEnumerator DashCooldown()
    {
        cooldown = true;
        yield return new WaitForSeconds(iFrameDuration);
        cooldown = false;
        yield break;
    }


    IEnumerator DashDelay()
    {
        //GetComponent<CircleCollider2D>().enabled = false;
        Dash = true;
        Physics2D.IgnoreLayerCollision(3, 7,true);
        damageManager.invincible = true;
        yield return new WaitForSeconds(dashDelay);
        damageManager.invincible = false;
        Physics2D.IgnoreLayerCollision(3, 7, false);
        print("DashFalse");
        
        Dash = false;
        //GetComponent<CircleCollider2D>().enabled = true;
        yield break;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            

            RaycastHit hit;
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);
            RaycastHit2D rayHit = Physics2D.Raycast(transform.position, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)+gameObject.transform.position),Mathf.Infinity, layerMask:~ignore);
            Debug.DrawLine(gameObject.transform.position, rayHit.point, color:Color.red);
            print("aiming" +rayHit.distance);
            audioManager.Slash();
        }
        if(rb.velocity.magnitude >= 1)
        {
            audioManager.walking = true;
        }
        else
        {
            audioManager.walking = false;
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryCanvas.SetActive(!inventoryCanvas.activeSelf);
        }
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

        if (Input.GetKeyDown(KeyCode.LeftShift) && !cooldown)
        {
            audioManager.Dash();
            StartCoroutine(DashDelay());
            rb.velocity *= 10;
            StartCoroutine(DashCooldown());
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
