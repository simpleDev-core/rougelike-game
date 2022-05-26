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
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Summon()
    {
        animator.Play(summonAnim, 0);
        Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + destructionDelay);
        Instantiate(summon, gameObject.transform.position, Quaternion.identity);
        

    }
}
