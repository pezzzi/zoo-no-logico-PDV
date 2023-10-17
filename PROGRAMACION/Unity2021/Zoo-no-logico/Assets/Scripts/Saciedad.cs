using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Saciedad : MonoBehaviour
{

    public Mati_CruzasAnimales[] cruzas;

    public int selectedJaula;

    public int saciedad;

    public Slider slider;

    public int comida;

    [SerializeField] private GameObject feedCount;

    [SerializeField] private Comida comidaHandler;

    // Start is called before the first frame update
    void Start()
    {
        cruzas = Resources.LoadAll<Mati_CruzasAnimales>("");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSaciedad(int jaula)
    {
        slider.value = PlayerPrefs.GetInt("SaciedadJaula" + jaula);
    }

    public void AddSaciedad()
    {
        saciedad = PlayerPrefs.GetInt("SaciedadJaula" + selectedJaula);
        comida = PlayerPrefs.GetInt("Comida");
        if (saciedad < 100)
        {
            print("Nombre: " + PlayerPrefs.GetString("Jaula" + selectedJaula));
            print("Saciedad antes: " + saciedad);
            PlayerPrefs.SetInt("SaciedadJaula" + selectedJaula, saciedad + 10);
            PlayerPrefs.SetInt("Comida", comida - 1);
            slider = GameObject.Find("BarraSaciedad" + selectedJaula).GetComponent<Slider>();
            Debug.Log(slider.value);
            SetSaciedad(selectedJaula);
            print("Saciedad despues: " + saciedad);
        } else
        {
            Debug.Log("la saciedad ya esta al maximo");
        }
    }

    public void AddToQueue()
    {
        if (PlayerPrefs.GetInt("Comida") > 0)
        {
            comidaHandler.SendMessage("SubtractComida");
            PlayerPrefs.SetInt("FeedJaula" + selectedJaula, PlayerPrefs.GetInt("FeedJaula" + selectedJaula) + 1);

            feedCount.GetComponent<TMP_Text>().text = (int.Parse(feedCount.GetComponent<TMP_Text>().text) + 1).ToString();
        }
       
    }

    public void SubtractFromQueue()
    {
        if (int.Parse(feedCount.GetComponent<TMP_Text>().text) > 0)
        {
            comidaHandler.SendMessage("AddComida");
            PlayerPrefs.SetInt("FeedJaula" + selectedJaula, PlayerPrefs.GetInt("FeedJaula" + selectedJaula) - 1);

            feedCount.GetComponent<TMP_Text>().text = (int.Parse(feedCount.GetComponent<TMP_Text>().text) - 1).ToString();
        }
    }
}
