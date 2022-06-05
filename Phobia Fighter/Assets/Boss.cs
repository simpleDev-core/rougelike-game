using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Boss : MonoBehaviour
{
    public BossSummoner summoner;
    public UnityEvent DeathEvent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy()
    {
        summoner.FinishSummon();
        DeathEvent.Invoke();
    }
}
