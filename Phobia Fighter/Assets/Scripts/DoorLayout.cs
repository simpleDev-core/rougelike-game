using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLayout : MonoBehaviour
{
    public float offset;
    public int xCount;
    public int yCount;
    public GameObject[] Rooms;
    public bool[] rotationalShuffle;
    // Start is called before the first frame update
    void Start()
    {
        for(int x = 0; x<xCount; x++)
        {
            for(int y = 0; y<yCount; y++)
            {
                int index = Mathf.RoundToInt(Random.Range(0, Rooms.Length));
                
                GameObject copy = Instantiate(Rooms[index], new Vector2(x * offset, y * offset), Quaternion.identity);
                copy.transform.Rotate(new Vector3(0, 0, 90 * Mathf.RoundToInt(Random.Range(0, 4))));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
