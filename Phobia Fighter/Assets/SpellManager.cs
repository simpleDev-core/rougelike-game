using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    public PlayerMovement playerManager;
    public GameObject lifestealEffect;
    public GameObject swordBloomSpawn;
    // Start is called before the first frame update
    void Start()
    {
        playerManager = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if(playerManager.spell != null)
            {
                if(playerManager.spell.name == "Lifesteal")
                {

                    DeductHealth(4);
                    StartCoroutine(lifesteal());
                }
                if (playerManager.spell.name == "Sword Bloom")
                {
                    DeductHealth(2);
                    Instantiate(swordBloomSpawn, gameObject.transform.position, Quaternion.identity);
                }
            }
            
        }
    }
    public void DeductHealth(float health)
    {
        if(playerManager.HasTalisman("HealthBoost"))
        {
            if(playerManager.HowManytalisman("HealthBoost") >= 2)
            {
                GetComponent<healthManager>().health -= health/2;
            }
            else
            {
                GetComponent<healthManager>().health -= health;
            }
        }
        else
        {
            GetComponent<healthManager>().health -= health;
        }
    }
    public IEnumerator lifesteal()
    {
        GetComponentInChildren<SwordManager>().stolenLife = 0.5f;
        lifestealEffect.SetActive(true);
        yield return new WaitForSeconds(10);
        lifestealEffect.SetActive(false);
        GetComponentInChildren<SwordManager>().stolenLife = 0f;
    }
}
