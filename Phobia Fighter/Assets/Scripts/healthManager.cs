using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Rendering;
public class healthManager : MonoBehaviour
{
    public float health = 12;
    public float maxHealth = 12;
    //public float tempHealth = 0;
    public Image healthBar;
    bool healthBarEnabled;
    public UnityEvent DamageEvent;
    public UnityEvent DeathEvent;
    Rigidbody2D rb;
    Volume pp;
    public GameObject healthCanvas;
    public bool alwaysShowBar;
    public bool invincible;
    public GameObject youDied;
    public float damageMod = 1;
    public bool laserProt;
    
    // Start is called before the firdst frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pp = GameObject.FindGameObjectWithTag("PostDamage").GetComponent<Volume>();
        if(healthBar != null)
        {
            healthBarEnabled = true;
        }
    }

    // Update is called once per frame
    public void Die()
    {
        if (youDied != null){
            Camera.main.gameObject.transform.parent = null;
            youDied.gameObject.SetActive(true);
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().DestroyAllBesidesPlayer();
        }
        
        Destroy(gameObject);
    }
    void Update()
    {
        if(health > maxHealth)
        {
            health = maxHealth;
        }
        if(health >= maxHealth && !alwaysShowBar)
        {
            healthCanvas.SetActive(false);
        }
        else
        {
            healthCanvas.SetActive(true);
        }
        if (healthBarEnabled)
        {
            healthBar.fillAmount = health/maxHealth;
        }
        if(health<= 0)
        {
            DeathEvent.Invoke();
        }
    }
    IEnumerator VolumeInterp(float fadeInTime, float fadeOutTime, float delay)
    {
        if(Mathf.RoundToInt(fadeInTime / Time.deltaTime) > 0)
        {
            int FadeInIndex = Mathf.RoundToInt(fadeInTime / Time.deltaTime);
            for (int x = 0; x <= FadeInIndex; x++)
            {
                pp.weight += 1 / fadeInTime / Time.deltaTime;
                if (pp.weight >= 1)
                {
                    pp.weight = 1;
                    break;
                }
                //print(x / fadeOutTime / Time.deltaTime);
                //print(x.ToString() + (fadeInTime / Time.deltaTime).ToString());
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }
        pp.weight = 1;
        yield return new WaitForSeconds(delay);
        int FadeOutIndex = Mathf.RoundToInt(fadeOutTime / Time.deltaTime);
        for (int x = 0; x <= FadeOutIndex; x++)
        {
            pp.weight -= 1/fadeOutTime;
            if(pp.weight <= 0)
            {
                pp.weight = 0;
                break;
            }
            //print(x / fadeOutTime / Time.deltaTime);
            //print(x.ToString() + (fadeOutTime / Time.deltaTime).ToString());
            yield return new WaitForSeconds(Time.deltaTime);
        }
        pp.weight = 0;
    }



    public void PostProcessingEdit()
    {
        StartCoroutine(VolumeInterp(0.1f, 0, 1f));
    }

    IEnumerator damageEffect()
    {
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 1;
    }

    public void Damage(float damage, GameObject instigator = null, string type = "normal")
    {
        if(invincible != true)
        {
            if(!(laserProt == true && type == "laser"))
            {
                if (GetComponent<AudioManager>() != null)
                {
                    GetComponent<AudioManager>().walkAudioScource.PlayOneShot(GetComponent<AudioManager>().damagedAudio);
                }
                if (damage > 0)
                {
                    DamageEvent.Invoke();

                }
                if(gameObject.tag == "Player")
                {
                    StartCoroutine(damageEffect());
                }
                health -= damage * damageMod;
                Vector3 offset = -instigator.transform.position + gameObject.transform.position;
                if (gameObject.tag == "Player")
                {
                    rb.velocity += new Vector2(offset.x, offset.y) * 10;
                }
                else
                {
                    rb.velocity += new Vector2(offset.x, offset.y) * 10;
                }
            }
            


        }   
    }
}
