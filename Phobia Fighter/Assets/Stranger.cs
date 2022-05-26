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
    // Update is called once per frame
    void Update()
    {
        if (!cooldown)
        {
            StartCoroutine(TP());
            cooldown = true;
        }
    }
}
