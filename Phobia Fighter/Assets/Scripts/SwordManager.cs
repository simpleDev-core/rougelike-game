using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordManager : MonoBehaviour
{
    public float damage;
    public SwordObject sword;
    public Vector2 hitboxDimensions;
    public Animator animator;
    Vector3 mouse_pos;
    public Transform target; //Assign to the object you want to rotate
    Vector3 object_pos;
    float angle;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mouse_pos = Input.mousePosition;
        mouse_pos.z = 5.23f; //The distance between the camera and object
        object_pos = Camera.main.WorldToScreenPoint(target.position);
        mouse_pos.x = mouse_pos.x - object_pos.x;
        mouse_pos.y = mouse_pos.y - object_pos.y;
        angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (Input.GetMouseButtonDown(0))
        {
            animator.Play("Slash");
            Collider2D[] colliders = Physics2D.OverlapBoxAll(gameObject.transform.position, hitboxDimensions, gameObject.transform.rotation.z);
            foreach(Collider2D collider in colliders)
            {
                if(collider.gameObject.GetComponent<healthManager>() != null && collider.gameObject.tag != "Player")
                {
                    
                    collider.gameObject.GetComponent<healthManager>().Damage(damage, gameObject);
                }
            }

        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, hitboxDimensions);
    }
}
