using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public bool homing;
    public bool homeOnEnemy = true;
    public float homeRange = 16;
    GameObject target;
    public float sensitivity = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        if (homeOnEnemy)
        {
            
            Collider2D[] colliders = Physics2D.OverlapCircleAll(gameObject.transform.position, homeRange);
            GameObject finalChoice = GameObject.FindGameObjectWithTag("Enemy");
            foreach (Collider2D collider in colliders)
            {
                if(collider.gameObject.tag == "Enemy" && Vector2.Distance(collider.transform.position,gameObject.transform.position)< Vector2.Distance(finalChoice.transform.position, gameObject.transform.position))
                {
                    finalChoice = collider.gameObject;
                }
                
            }
            target = finalChoice;
            print(target.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newVeloc = gameObject.transform.right * speed;
        rb.velocity = newVeloc;
        if (homing)
        {
            Vector3 myLocation = transform.position;
            Vector3 targetLocation = target.transform.position;
            targetLocation.z = myLocation.z; // ensure there is no 3D rotation by aligning Z position

            // vector from this object towards the target location
            Vector3 vectorToTarget = targetLocation - myLocation;
            // rotate that vector by 90 degrees around the Z axis
            Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * vectorToTarget;

            // get the rotation that points the Z axis forward, and the Y axis 90 degrees away from the target
            // (resulting in the X axis facing the target)
            Quaternion targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: rotatedVectorToTarget);

            
            Quaternion rotation = Quaternion.FromToRotation(transform.rotation.eulerAngles, targetRotation.eulerAngles * sensitivity);
            rb.angularVelocity += rotation.eulerAngles.z;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
