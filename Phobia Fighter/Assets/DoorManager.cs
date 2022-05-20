using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public GameObject[] PossibleRooms;
    public bool blocked;
    public int maxRooms;
    public Transform CopyRoot;
    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.FindGameObjectsWithTag("RoomBlocker").Length > maxRooms)
        {
            gameObject.GetComponent<DoorManager>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        if (!blocked )
        {
            //Mathf.RoundToInt(Random.Range(0, PossibleRooms.Length-1))
            GameObject room = Instantiate(PossibleRooms[Mathf.RoundToInt(Random.Range(0, PossibleRooms.Length))], transform);
            room.transform.parent = GameObject.FindGameObjectWithTag("RoomRoot").transform;
            gameObject.GetComponent<DoorManager>().enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "RoomBlocker")
        {
            blocked = true;
            print(collision.gameObject.name);
            gameObject.GetComponent<DoorManager>().enabled = false;
        }
    }
}
