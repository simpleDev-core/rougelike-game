using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class RoomSpawnInfo : ScriptableObject
{
    [Range(0.0f, 1.0f)]
    public float spawnWeight;

    public bool lockedWeight;
    public GameObject spawnObject;
    public bool randomRotation;
    
}
