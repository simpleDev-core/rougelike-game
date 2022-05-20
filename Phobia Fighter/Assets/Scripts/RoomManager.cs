using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public Transform[] Doors;
    public GameObject root;
    public float playerActivationDistance;
    public bool activated;
    public bool visible;
    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector2.Distance(player.position, gameObject.transform.position)<= playerActivationDistance || 

        if (visible)
        {
            if (!root.activeInHierarchy)
            {
                root.SetActive(true);
            }

        }
        else
        {
            if (root.activeInHierarchy)
            {
                root.SetActive(false);
            }
        }
    }
    private void OnBecameVisible()
    {
        visible = true;
    }
    private void OnBecameInvisible()
    {
        visible = false;
    }
}
