using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
[RequireComponent(typeof(AudioSource))]
public class Interactible : MonoBehaviour
{
    public float interactionTime = 1;
    public Image fillBar;
    bool playerOverlap;
    float fillTime;
    AudioSource chargeAudio;
    public AudioClip verifyClip;
    bool filled;
    public UnityEvent verifiedEvent;
    // Start is called before the first frame update
    void Start()
    {
        chargeAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(KeyCode.E) && !filled)
        {
            chargeAudio.pitch = (1 / chargeAudio.clip.length) * interactionTime * 2;
            fillTime += Time.deltaTime*interactionTime;
            if (!chargeAudio.isPlaying)
            {
                chargeAudio.Play();
            }
            if(fillTime >= 1)
            {
                chargeAudio.pitch = 1;
                fillBar.transform.parent.gameObject.SetActive(false);
                chargeAudio.clip = verifyClip;
                filled = true;
                chargeAudio.Play();
                verifiedEvent.Invoke();
                return;
            }
            
        }
        else
        {
            if (!filled)
            {
                chargeAudio.Stop();
                if (fillTime <= 0)
                {
                    fillTime = 0;
                }
                else
                {
                    fillTime -= Time.deltaTime * interactionTime;
                }
            }
            
            
        }
        fillBar.fillAmount = fillTime / interactionTime;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!filled)
        {
            if (collision.gameObject.tag == "Player")
            {
                playerOverlap = false;
                fillBar.transform.parent.gameObject.SetActive(false);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!filled)
        {
            if (collision.gameObject.tag == "Player")
            {
                playerOverlap = true;
                fillBar.transform.parent.gameObject.SetActive(true);
            }
        }
    }
}
