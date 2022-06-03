using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(healthManager))]

public class TalismanManager : MonoBehaviour
{
    PlayerMovement playerManager;
    healthManager healthManager;
    public Volume furyVolume;
    public AudioSource furyAudio;
    // Start is called before the first frame update
    void Start()
    {
        playerManager = GetComponent<PlayerMovement>();
        healthManager = GetComponent<healthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        OnUpdateTalisman(playerManager.talisman1);
        OnUpdateTalisman(playerManager.talisman2);
        if (playerManager.HasTalisman("Fury"))
        {
            
            if (healthManager.health < healthManager.maxHealth / 4)
            {
                furyVolume.weight = 1;
                furyAudio.Play();
                if (playerManager.HowManytalisman("Fury") == 1)
                {
                    GetComponentInChildren<SwordManager>().damageMult = 2;
                }
                else
                {
                    GetComponentInChildren<SwordManager>().damageMult = 4;
                }
            }
            else
            {
                furyVolume.weight = 0;
                furyAudio.Pause();
                GetComponentInChildren<SwordManager>().damageMult = 1;
            }


        }
        else
        {
            furyVolume.weight = 0;
            furyAudio.Pause();
            GetComponentInChildren<SwordManager>().damageMult = 1;
        }
    }
    public void OnUpdateTalisman(Talisman talisman)
    {
        if(talisman != null)
        {
            if (talisman.name == "HealthBoost")
            {
                if (healthManager.health < healthManager.maxHealth)
                {
                    healthManager.health += Time.deltaTime / 5;
                }


            }
            
        }
        
    }
}
