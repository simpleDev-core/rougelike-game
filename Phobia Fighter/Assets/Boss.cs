using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Boss : MonoBehaviour
{
    public BossSummoner summoner;
    public UnityEvent DeathEvent;
    public string deathType;
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
        if(deathType == "MrPitch")
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().MrPitch = true;
        }
        if (deathType == "LongWords")
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().BigWord = true;
        }
        if (deathType == "Everchase")
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().Everchase = true;
        }
    }
}
