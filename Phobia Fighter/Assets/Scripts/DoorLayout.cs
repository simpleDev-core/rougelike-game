using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLayout : MonoBehaviour
{
    public float offset;
    public int xCount;
    public int yCount;
    public GameObject[] Rooms;
    public GameObject[] oneOffRooms;
    public List<Vector2> blacklist = new List<Vector2>();
    public bool[] rotationalShuffle;
    // Start is called before the first frame update
    void Start()
    {
        for(int x = 0; x<xCount; x++)
        {
            for(int y = 0; y<yCount; y++)
            {
                bool not = false;
                foreach(Vector2 vector in blacklist)
                {
                    if(x == vector.x && y == vector.y)
                    {
                        not = true;
                        //return;
                    }
                }
                if (!not)
                {
                    int index = Mathf.RoundToInt(Random.Range(0, Rooms.Length));

                    GameObject copy = Instantiate(Rooms[index], new Vector2(x * offset, y * offset), Quaternion.identity);
                    Room info = copy.GetComponent<DoorInfo>().info;

                    bool rotate = true;
                    if (info.dimensions.x > 1)
                    {
                        rotate = false;
                        blacklist.Add(new Vector2(x + info.dimensions.x - 1, y));
                    }
                    if (info.dimensions.y > 1)
                    {
                        rotate = false;
                        blacklist.Add(new Vector2(x, y + info.dimensions.y - 1));
                    }
                    if (info.dimensions.y > 1 && info.dimensions.x > 1)
                    {
                        rotate = false;
                        blacklist.Add(new Vector2(x + info.dimensions.x - 1, y + info.dimensions.y - 1));
                    }

                    print("added blacklist" + new Vector2(x + info.dimensions.x - 1, y).ToString());
                    if (rotate)
                    {
                        copy.transform.Rotate(new Vector3(0, 0, 90 * Mathf.RoundToInt(Random.Range(0, 4))));
                    }
                }
                
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
