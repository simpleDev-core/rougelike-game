using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    
    public AudioClip dashAudio;
    public AudioClip[] slashAudio;
    public AudioClip damagedAudio;
    public AudioSource walkAudioScource;

    public bool walking;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (walking)
        {
            if (!walkAudioScource.isPlaying)
            {
                walkAudioScource.Play();
            }
        }
        else
        {
            walkAudioScource.Pause();
        }
    }
    public void Dash()
    {
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.PlayOneShot(dashAudio);
    }
    public void Slash()
    {
        print("alask");
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.PlayOneShot(slashAudio[Mathf.RoundToInt(Random.Range(0,slashAudio.Length-1))]);
    }
    public void Damage()
    {
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.PlayOneShot(damagedAudio);
    }
}
