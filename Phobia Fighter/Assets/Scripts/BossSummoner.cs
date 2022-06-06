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
    public float brightness = 0.1f;
    float oldBright;
    float shadowIntense;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerLight = player.GetComponentInChildren<UnityEngine.Rendering.Universal.Light2D>();
        ambiance = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>();
        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        shadowIntense = playerLight.shadowIntensity;
        oldBright = playerLight.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Summon();
            gameObject.GetComponent<SphereCollider>().enabled = false;
        }
    }
    public void setPlayerLight(float intensity)
    {
        
        playerLight.intensity = intensity;
        playerLight.shadowIntensity = 1;
    }
    IEnumerator dissapear()
    {
        yield return new WaitForSeconds(this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + destructionDelay);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<UnityEngine.Rendering.Universal.Light2D>().enabled = false;
        yield break;
    }
    public void Summon()
    {
        animator.Play(summonAnim, 0);
        StartCoroutine(dissapear());
        //Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + destructionDelay);
        GameObject clone = Instantiate(summon, gameObject.transform.position, Quaternion.identity);
        clone.GetComponent<Boss>().summoner = gameObject.GetComponent<BossSummoner>();
        ambiance.Pause();
    }
    public void FinishSummon()
    {
        setPlayerLight(oldBright);
        playerLight.shadowIntensity = shadowIntense;
        ambiance.Play();
    }
}
