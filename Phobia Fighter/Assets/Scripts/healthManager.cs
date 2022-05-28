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
        }
        
        Destroy(gameObject);
    }
    void Update()
    {
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
                yield return new WaitForEndOfFrame();
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
            yield return new WaitForEndOfFrame();
        }
        pp.weight = 0;
    }
    public void PostProcessingEdit()
    {
        StartCoroutine(VolumeInterp(0.01f, 0, 0.5f));
    }
    public void Damage(float damage, GameObject instigator = null)
    {
        if(invincible != true)
        {
            if(GetComponent<AudioManager>()!= null)
            {
                GetComponent<AudioManager>().walkAudioScource.PlayOneShot(GetComponent<AudioManager>().damagedAudio);
            }
            if (damage > 0)
            {
                DamageEvent.Invoke();

            }

            health -= damage;
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
