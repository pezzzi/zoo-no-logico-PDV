using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Saciedad : MonoBehaviour
{

    public Mati_CruzasAnimales[] cruzas;

    public int selectedJaula;

    public int saciedad;

    public Slider slider;

    public Image sliderFill;

    public int comida;

    public GameObject PantallaFaltaComida;

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

    public void AddSaciedadByJaula(int jaula, int feedCount)
    {
        for (int i = 0; i < feedCount; i++)
        {
            saciedad = PlayerPrefs.GetInt("SaciedadJaula" + jaula);
            comida = PlayerPrefs.GetInt("Comida");
            if (saciedad < 100)
            {
                print("Nombre: " + PlayerPrefs.GetString("Jaula" + jaula));
                print("Saciedad antes: " + saciedad);
                PlayerPrefs.SetInt("SaciedadJaula" + jaula, saciedad + 10);
                //PlayerPrefs.SetInt("Comida", comida - 1);
                //slider = GameObject.Find("BarraSaciedad" + jaula).GetComponent<Slider>();
                //Debug.Log(slider.value);
                //SetSaciedad(jaula);
                print("Saciedad despues: " + saciedad);
            }
        }
    }

    public void AddToQueue()
    {
        print(PlayerPrefs.GetInt("SaciedadJaula" + selectedJaula));
        if ((PlayerPrefs.GetInt("SaciedadJaula" + selectedJaula) + (PlayerPrefs.GetInt("FeedJaula" + selectedJaula) * 10)) <= 110)
        {
            if (PlayerPrefs.GetInt("Comida") > 0)
            {
                comidaHandler.SendMessage("SubtractComida");
                PlayerPrefs.SetInt("FeedJaula" + selectedJaula, PlayerPrefs.GetInt("FeedJaula" + selectedJaula) + 1);

                feedCount.GetComponent<TMP_Text>().text = (int.Parse(feedCount.GetComponent<TMP_Text>().text) + 1).ToString();
            } else
            {
                Debug.Log("No tienes más comida");
            }
        } else
        {
            Debug.Log("El animal ya estará en 100 de saciedad");
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

    public void AutoFeed()
    {
        int foodPerCage = (int)MathF.Floor(PlayerPrefs.GetInt("Comida") / PlayerPrefs.GetInt("JaulasOcupadas"));
        foodPerCage = Math.Max(foodPerCage, 1);
        for (int i = 0; i < PlayerPrefs.GetInt("JaulasOcupadas"); i++)
            {
                if (PlayerPrefs.GetInt("JaulaActiva" + i) == 1)
                {
                    if ((PlayerPrefs.GetInt("SaciedadJaula" + i) + (PlayerPrefs.GetInt("FeedJaula" + i) * 10)) <= 110)
                    {
                        for (int e = 0; e < foodPerCage; e++)
                        {
                            if (PlayerPrefs.GetInt("Comida") > 0)
                            {
                                comidaHandler.SendMessage("SubtractComida");
                                PlayerPrefs.SetInt("FeedJaula" + i, PlayerPrefs.GetInt("FeedJaula" + i) + 1);
                                feedCount = GameObject.Find("feed" + i);
                                feedCount.GetComponent<TMP_Text>().text = (int.Parse(feedCount.GetComponent<TMP_Text>().text) + 1).ToString();
                            }
                            else
                            {
                                PantallaFaltaComida.SetActive(true);
                                Debug.Log("No tienes más comida");
                            break;
                            }
                        }
                    }
                    else
                    {
                        Debug.Log("El animal ya estará en 100 de saciedad");
                    }
                }
            }
    }

    public void HidePantallaFaltaComida()
    {
        PantallaFaltaComida.SetActive(false);
    }
}
