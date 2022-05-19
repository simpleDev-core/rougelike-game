using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordManager : MonoBehaviour
{
    public float damage;
    public enum swordType { base_sword, tuning_fork};
    public swordType sword;
    public Vector2 hitboxDimensions;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Physics2D.OverlapBox(gameObject.transform.position, hitboxDimensions, 0);

        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, hitboxDimensions);
    }
}
