using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbushRoom : MonoBehaviour
{
    public int enemiesPresent;
    AmbushWall[] walls;
    // Start is called before the first frame update
    void Start()
    {
        walls = GetComponentsInChildren<AmbushWall>();
    }

    // Update is called once per frame
    void Update()
    {
        //print(GetComponentsInChildren<AmbushWall>().Length);
        if (enemiesPresent <= 0)
        {
            enemiesPresent = 0;
            
            foreach (AmbushWall wall in walls)
            {
                wall.gameObject.SetActive(false);
            }
            
        }
        else
        {
            //enemiesPresent = 0;

            foreach (AmbushWall wall in walls)
            {
                wall.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && enemiesPresent >= 1)
        {
            print("AMBUSH");
            foreach(AmbushWall wall in walls)
            {
                wall.Ambush();
            }
        }
        if(collision.gameObject.tag == "Enemy")
        {
            enemiesPresent++;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && enemiesPresent >= 1)
        {
            foreach (AmbushWall wall in walls)
            {
                wall.Ambush();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            enemiesPresent--;
        }
    }
}
