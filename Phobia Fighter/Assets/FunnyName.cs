using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FunnyName : MonoBehaviour
{
    public TMPro.TMP_Text text;
    float delay = 5;
    int index = 1;
    // Start is called before the first frame update
    void Start()
    {
        AddText();
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(delay);
        delay = delay / 2;
        index++;
        AddText();
        yield break;
    }
    void AddText()
    {
        if(text.text.Length >= 2000)
        {
            text.text = RandomStringGenerator(2000);
        }
        else
        {
            text.text += RandomStringGenerator(index);
        }
        
        StartCoroutine(Delay());
    }
    // Update is called once per frame
    void Update()
    {
        if(index >= 36)
        {
            text.fontSize = Random.Range(36, index);
            text.characterSpacing = Random.Range(0, 4);
            
        }
    }
    string RandomStringGenerator(int lenght)
    {
        string characters = "q w e r t y u i o p a s d f h g j k l z x c v b n m";
        string generated_string = "";

        for (int i = 0; i < lenght; i++)
            generated_string += characters.Split(" ")[Random.Range(0, characters.Split(" ").Length)];

        return generated_string;
    }
}
