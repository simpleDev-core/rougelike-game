using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stranger : MonoBehaviour
{
    public float maxTeleportTime = 20;
    public GameObject Projectile;
    public GameObject[] TeleportSpawns;
    public float radius = 16;
    bool cooldown;
    public Transform projectileAnchor;
    bool shootCooldown;
    public float projectileDelay;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    IEnumerator TP()
    {
        Vector3 playerRandomPos = Random.insideUnitCircle * radius;
        gameObject.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position + playerRandomPos;
        yield return new WaitForSeconds(Random.Range(0,maxTeleportTime));
        Instantiate(TeleportSpawns[Mathf.RoundToInt(Random.Range(0, TeleportSpawns.Length - 1))], gameObject.transform.position, Quaternion.identity);
        cooldown = false;
    }
    IEnumerator ProjectileShoot()
    {
        shootCooldown = true;
        yield return new WaitForSeconds(projectileDelay);
        Instantiate(Projectile, projectileAnchor.transform.position, projectileAnchor.transform.rotation);
        shootCooldown = false;
        
    }
    // Update is called once per frame
    void Update()
    {
        projectileAnchor.RotateAround(point: gameObject.transform.position, axis: new Vector3(0, 0, 1), Time.deltaTime * 90);
        if (!cooldown)
        {
            StartCoroutine(TP());
            cooldown = true;
            
        }
        if (!shootCooldown)
        {
            StartCoroutine(ProjectileShoot());
        }
        
    }
}
