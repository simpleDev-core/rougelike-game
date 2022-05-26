using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

public class BossSummoner : MonoBehaviour
{
    public GameObject summon;
    public string summonAnim;
    SpriteRenderer renderer;
    Animator animator;
    bool summoned;
    public float destructionDelay;
    GameObject player;
    AudioSource ambiance;
    UnityEngine.Rendering.Universal.Light2D playerLight;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerLight = player.GetComponentInChildren<UnityEngine.Rendering.Universal.Light2D>();
        ambiance = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>();
        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setPlayerLight(float intensity)
    {
        playerLight.intensity = intensity;
        playerLight.shadowIntensity = 1;
    }
    public void Summon()
    {
        animator.Play(summonAnim, 0);
        Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + destructionDelay);
        Instantiate(summon, gameObject.transform.position, Quaternion.identity);
        ambiance.Pause();


    }
}
