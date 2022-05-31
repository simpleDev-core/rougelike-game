using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordManager : MonoBehaviour
{
    public float damage;
    public SwordObject sword;
    //public Vector2 hitboxDimensions;
    public Vector2 tapering;
    public Animator animator;
    Vector3 mouse_pos;
    public Transform target; //Assign to the object you want to rotate
    Vector3 object_pos;
    float angle;
    public int hitResolution;
    public float hitLength;
    public PlayerMovement playerScript;
    //public float angleOffset;
    public Transform gizmoAnchor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gizmoAnchor.transform.localPosition = sword.hitAnchorPosition;
        mouse_pos = Input.mousePosition;
        mouse_pos.z = 5.23f; //The distance between the camera and object
        object_pos = Camera.main.WorldToScreenPoint(target.position);
        mouse_pos.x = mouse_pos.x - object_pos.x;
        mouse_pos.y = mouse_pos.y - object_pos.y;
        angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + sword.offsetAngle)) ;

        if (Input.GetMouseButtonDown(0) && sword != null)
        {
            playerScript.audioManager.slashAudio = sword.swordClips;
            playerScript.audioManager.Slash();
            animator.Play(sword.animName[Random.Range(0,sword.animName.Length)]);
            Vector3 hitbox3D = sword.hitBoxDimensions / 2;
            Collider2D[] colliders = Physics2D.OverlapBoxAll(gizmoAnchor.position, sword.hitBoxDimensions, gameObject.transform.rotation.z);
            //List<Collider2D> colliders = new List<Collider2D>();
            /*for (int i = 0; i < 10; i++)
            {
                //Debug.DrawRay(gameObject.transform.position, transform.right * tapering.x + (transform.up * (i - hitResolution / 2) * tapering.y, Color.green, 100);
                RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, transform.right * tapering.x + (transform.up * (i - hitResolution / 2) * tapering.y), hitLength, layerMask: 6);
                colliders.Add(hit.collider);

            }*/
            
            print(colliders.Length);
            foreach(Collider2D collider in colliders)
            {
                print(collider.name);
                if(collider.gameObject.GetComponent<healthManager>() != null && collider.gameObject.tag != "Player")
                {
                    print("DAMAGE MANAGER");
                    collider.gameObject.GetComponent<healthManager>().Damage(sword.damage, gameObject);
                }
            }

        }
    }
    private void OnDrawGizmos()
    {
        gizmoAnchor.transform.localPosition = sword.hitAnchorPosition;
        Gizmos.DrawWireCube(gizmoAnchor.position, sword.hitBoxDimensions);
        Vector3 hitbox3D = sword.hitBoxDimensions / 2;
        Gizmos.color = Color.green;
        for (int i = 0; i < hitResolution; i++)
        {
            Gizmos.DrawRay( gameObject.transform.position, transform.right * tapering.x + (transform.up * (i - hitResolution/2) * tapering.y));
        }
    }

}
