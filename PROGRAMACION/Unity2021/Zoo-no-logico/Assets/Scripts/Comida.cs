using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Comida : MonoBehaviour
{

    public int comida;

    public Text foodCount;

    // Start is called before the first frame update
    void Start()
    {
        comida = PlayerPrefs.GetInt("Comida");
        foodCount = GameObject.Find("Food count").GetComponent<UnityEngine.UI.Text>();
        SetComida(comida);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetComida(int cantComida)
    {
        PlayerPrefs.SetInt("Comida", comida);
        foodCount.text = cantComida.ToString();
        Debug.Log("Set texto comida " + comida);
    }

    public void AddComida()
    {
        comida = PlayerPrefs.GetInt("Comida");
        comida++;

        SetComida(comida);
    }

    public void AddPackComida()
    {
        comida = PlayerPrefs.GetInt("Comida");
        comida = comida + 5;

        SetComida(comida);
    }

    public void SubtractComida()
    {
        comida = PlayerPrefs.GetInt("Comida");
        comida--;

        SetComida(comida);
    }
}
