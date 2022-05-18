using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class healthManager : MonoBehaviour
{
    public float health = 12;
    public float maxHealth = 12;
    public Image healthBar;
    bool healthBarEnabled;
    // Start is called before the first frame update
    void Start()
    {
        if(healthBar != null)
        {
            healthBarEnabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (healthBarEnabled)
        {
            healthBar.fillAmount = health/maxHealth;
        }
    }
}
