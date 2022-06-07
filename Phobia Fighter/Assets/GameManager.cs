using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool Everchase;
    public bool MrPitch;
    public bool BigWord;
    public int killedBosses;
    public bool destroyAll;
    public GameObject whiteDoor;
    public float spawnRadius;
    bool win;
    public GameObject death;
    GameObject player;
    public AudioClip NOISE;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (destroyAll)
        {
            DestroyAllBesidesPlayer();
            destroyAll = false;
        }
        if (Everchase && MrPitch && BigWord && !win)
        {
            Win();
            win = true;
        }
        if(Vector3.Distance(player.transform.position, transform.position) >= spawnRadius && Time.frameCount % (Mathf.RoundToInt(3/Time.deltaTime)) == 0)
        {
            Vector3 random = Random.insideUnitCircle;
            GameObject clone = Instantiate(death, player.transform.position + random * 64, Quaternion.identity);
            clone.transform.localScale = Vector3.one * Random.value;
            if (GetComponent<AudioSource>() == null)
            {
                AudioSource noise = gameObject.AddComponent<AudioSource>();
                GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>().Pause();
                noise.loop = true;
                noise.clip = NOISE;
                noise.Play();
            }
            
        }
    }

    public void Win()
    {
        DestroyAllBesidesPlayer();
        Instantiate(whiteDoor, player.transform.position + new Vector3(0, 3, 0), Quaternion.identity);
    }

    public void DestroyAllBesidesPlayer()
    {
        GameObject[] objects = GameObject.FindObjectsOfType<GameObject>();
        foreach(GameObject Object in objects)
        {
            
            if(Object.tag != "Player" && Object.tag != "DND" && Object.tag != "GameManager" && Object.tag != "Sword" && Object.tag != "InventoryCanvas" && Object.tag != "MainCamera")
            {
                Destroy(Object);
            }

        }
        if (GetComponent<AudioSource>() != null)
        {
            GetComponent<AudioSource>().enabled = false;
        }
        //GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>().Pause();
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(gameObject.transform.position, spawnRadius);

    }
}
