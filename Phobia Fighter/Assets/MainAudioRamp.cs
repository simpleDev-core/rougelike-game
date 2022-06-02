using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainAudioRamp : MonoBehaviour
{
    public float range;
    AudioSource mainAudio;
    AudioSource seekerAudio;
    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        mainAudio = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>();
        seekerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(gameObject.transform.position,player.position) <= range)
        {
            mainAudio.volume = range / Vector3.Distance(gameObject.transform.position, player.position);
        }
        else
        {
            mainAudio.volume = 1;
        }
    }
}
