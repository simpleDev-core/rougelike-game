using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SwordObject : ScriptableObject
{
    public new string name;
    public Sprite swordImage;
    public string[] animName;
    public float damage;
    public Vector2 hitBoxDimensions;
    public Vector2 hitAnchorPosition;
    public float offsetAngle;
    public AudioClip[] swordClips;

}
