using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLayout : MonoBehaviour
{
    public float offset;
    public int xCount;
    public int yCount;
    public RoomSpawnWeighting[] Rooms;
    public RoomSpawnWeighting[] oneOffRooms;
    public List<Vector2> blacklist = new List<Vector2>();
    //public bool[] rotationalShuffle;
    List<RoomSpawnWeighting> roomSpawnsQueue = new List<RoomSpawnWeighting>();
    // Start is called before the first frame update


    [System.Serializable]
    public class RoomSpawnWeighting
    {

        public GameObject room;
        public float weight;
        public bool rotation;

        public RoomSpawnWeighting(GameObject room, float weight, bool rotation)
        {
            this.room = room;
            this.rotation = rotation;
            this.weight = weight;
        }
    }


    void Start()
    {
        
        for(int i = 0; i < xCount * yCount; i++)
        {
            print(Rooms[Mathf.RoundToInt(Random.Range(0, Rooms.Length - 1))].room);
            roomSpawnsQueue.Add(Rooms[GetRandomWeightedIndex(Rooms)]);
            
        }
        /*for(int i = 0; i < oneOffRooms.Length; i++)
        {
            roomSpawnsQueue.RemoveAt(0);
        }*/
        foreach(RoomSpawnWeighting roomSpawn in oneOffRooms)
        {
            roomSpawnsQueue.Add(roomSpawn);
        }

        for (int i = 0; i < roomSpawnsQueue.Count; i++)
        {
            RoomSpawnWeighting temp = roomSpawnsQueue[i];
            int randomIndex = Random.Range(i, roomSpawnsQueue.Count);
            roomSpawnsQueue[i] = roomSpawnsQueue[randomIndex];
            roomSpawnsQueue[randomIndex] = temp;
        }

        int index = 0;
        int x = 0;
        while (true)
        {
            for(int y = 0; y < yCount; y++)
            {
                bool skip = false;
                foreach(Vector2 vector in blacklist)
                {
                    if(new Vector2(x,y) == vector)
                    {
                        skip = true;
                    }
                }
                if (roomSpawnsQueue[index] != null)
                {
                    if (skip)
                    {
                        print("skip on " + x + "," + y);
                    }
                    else
                    {
                        GameObject clone;
                        if (roomSpawnsQueue[index].room != null)
                        {
                            clone = Instantiate(roomSpawnsQueue[index].room, new Vector3(x * offset, y * offset, 0), Quaternion.identity);
                            if (roomSpawnsQueue[index].rotation)
                            {
                                clone.transform.Rotate(new Vector3(0, 0, 90 * Mathf.RoundToInt(Random.Range(0, 5))));
                            }
                        }
                        else
                        {
                            int weightedInd = GetRandomWeightedIndex(Rooms);
                            clone = Instantiate(Rooms[weightedInd].room, new Vector3(x * offset, y * offset, 0), Quaternion.identity);
                            if (Rooms[weightedInd].rotation)
                            {
                                clone.transform.Rotate(new Vector3(0, 0, 90 * Mathf.RoundToInt(Random.Range(0, 5))));
                            }
                        }
                        Room room = clone.GetComponent<DoorInfo>().info;
                        if (room.dimensions.x > 1)
                        {
                            //rotate = false;
                            blacklist.Add(new Vector2(x + room.dimensions.x - 1, y));
                        }
                        if (room.dimensions.y > 1)
                        {
                            //room = false;
                            blacklist.Add(new Vector2(x, y + room.dimensions.y - 1));
                        }
                        if (room.dimensions.y > 1 && room.dimensions.x > 1)
                        {
                            //room = false;
                            blacklist.Add(new Vector2(x + room.dimensions.x - 1, y + room.dimensions.y - 1));
                        }

                        index++;
                    }
                }
            }
            x++;
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int GetRandomWeightedIndex(RoomSpawnWeighting[] weights)
    {
        if (weights == null || weights.Length == 0) return -1;

        float w;
        float t = 0;
        int i;
        for (i = 0; i < weights.Length; i++)
        {
            w = weights[i].weight;

            if (float.IsPositiveInfinity(w))
            {
                return i;
            }
            else if (w >= 0f && !float.IsNaN(w))
            {
                t += weights[i].weight;
            }
        }

        float r = Random.value;
        float s = 0f;

        for (i = 0; i < weights.Length; i++)
        {
            w = weights[i].weight;
            if (float.IsNaN(w) || w <= 0f) continue;

            s += w / t;
            if (s >= r) return i;
        }

        return -1;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(new Vector3(xCount * offset / 2, yCount * offset / 2, 0), new Vector3(xCount * offset, yCount * offset, 0));
    }
    //public void OldRoomGen()
    //{
    //    for (int x = 0; x < xCount; x++)
    //    {
    //        for (int y = 0; y < yCount; y++)
    //        {
    //            bool not = false;
    //            foreach (Vector2 vector in blacklist)
    //            {
    //                if (x == vector.x && y == vector.y)
    //                {
    //                    not = true;
    //                    //return;
    //                }
    //            }
    //            if (!not)
    //            {
    //                int index = Mathf.RoundToInt(Random.Range(0, Rooms.Length));
    //                GameObject copy;
    //                if (Rooms[index] != null || oneOffRooms.Count < 1)
    //                {
    //                    if (Rooms[index] != null)
    //                    {
    //                        copy = Instantiate(Rooms[index].spawnObject, new Vector2(x * offset, y * offset), Quaternion.identity);
    //                        Room info = copy.GetComponent<DoorInfo>().info;

    //                        bool rotate = Rooms[index].randomRotation;
    //                        if (info.dimensions.x > 1)
    //                        {
    //                            rotate = false;
    //                            blacklist.Add(new Vector2(x + info.dimensions.x - 1, y));
    //                        }
    //                        if (info.dimensions.y > 1)
    //                        {
    //                            rotate = false;
    //                            blacklist.Add(new Vector2(x, y + info.dimensions.y - 1));
    //                        }
    //                        if (info.dimensions.y > 1 && info.dimensions.x > 1)
    //                        {
    //                            rotate = false;
    //                            blacklist.Add(new Vector2(x + info.dimensions.x - 1, y + info.dimensions.y - 1));
    //                        }

    //                        print("added blacklist" + new Vector2(x + info.dimensions.x - 1, y).ToString());
    //                        if (rotate)
    //                        {
    //                            copy.transform.Rotate(new Vector3(0, 0, 90 * Mathf.RoundToInt(Random.Range(0, 4))));
    //                        }
    //                    }

    //                }
    //                else
    //                {
    //                    print("addingOneOff");
    //                    index = Random.Range(0, oneOffRooms.Count - 1);
    //                    copy = Instantiate(oneOffRooms[index], new Vector2(x * offset, y * offset), Quaternion.identity);
    //                    oneOffRooms.RemoveAt(index);
    //                    print("OneOffAdded");
    //                    Room info = copy.GetComponent<DoorInfo>().info;

    //                    bool rotate = true;
    //                    if (info.dimensions.x > 1)
    //                    {
    //                        rotate = false;
    //                        blacklist.Add(new Vector2(x + info.dimensions.x - 1, y));
    //                    }
    //                    if (info.dimensions.y > 1)
    //                    {
    //                        rotate = false;
    //                        blacklist.Add(new Vector2(x, y + info.dimensions.y - 1));
    //                    }
    //                    if (info.dimensions.y > 1 && info.dimensions.x > 1)
    //                    {
    //                        rotate = false;
    //                        blacklist.Add(new Vector2(x + info.dimensions.x - 1, y + info.dimensions.y - 1));
    //                    }

    //                    print("added blacklist" + new Vector2(x + info.dimensions.x - 1, y).ToString());
    //                    if (rotate)
    //                    {
    //                        copy.transform.Rotate(new Vector3(0, 0, 90 * Mathf.RoundToInt(Random.Range(0, 4))));
    //                    }
    //                }


    //            }

    //        }
    //    }
    //}
}
