using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordManager : MonoBehaviour
{
    public float damage;
    public enum swordType { base_sword, tuning_fork};
    public swordType sword;
    public Vector2 hitboxDimensions;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
