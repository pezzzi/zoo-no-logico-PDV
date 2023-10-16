using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comida : MonoBehaviour
{

    public int comida;

    public GameObject foodCount;

    // Start is called before the first frame update
    void Start()
    {
        comida = PlayerPrefs.GetInt("Comida");
        GameObject.Find("Food count").GetComponent<UnityEngine.UI.Text>().text = comida.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddComida()
    {
        comida = PlayerPrefs.GetInt("Comida");
        PlayerPrefs.SetInt("Comida", comida + 1);

        GameObject.Find("Food count").GetComponent<UnityEngine.UI.Text>().text = comida.ToString();
    }

    public void SubtractComida()
    {
        comida = PlayerPrefs.GetInt("Comida");
        PlayerPrefs.SetInt("Comida", comida - 1);

        GameObject.Find("Food count").GetComponent<UnityEngine.UI.Text>().text = comida.ToString();
    }
}
