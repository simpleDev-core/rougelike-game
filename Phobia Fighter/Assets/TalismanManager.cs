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
            if (talisman.name == "Fury")
            {
                if (healthManager.health < healthManager.maxHealth / 4)
                {
                    furyVolume.weight = 1;
                    GetComponentInChildren<SwordManager>().damageMult = 2;
                }
                else
                {
                    furyVolume.weight = 0;
                    GetComponentInChildren<SwordManager>().damageMult = 1;
                }


            }
        }
        
    }
}
