﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JaulaManager : MonoBehaviour {



    public GameObject Jaula1;
    public GameObject Jaula2;
    public GameObject Jaula3;
    public GameObject Jaula4;
    public GameObject Jaula5;
    public GameObject Jaula6;
    public GameObject Jaula7;
    public GameObject Jaula8;
    public GameObject Jaula9;
    public GameObject Jaula10;
    public GameObject Jaula11;
    public GameObject Jaula12;
    public GameObject Jaula13;
    public GameObject Jaula14;
    public GameObject Jaula15;
    public GameObject Jaula16;
    public GameObject Jaula17;
    public GameObject Jaula18;
    public GameObject Jaula19;
    public GameObject Jaula20;

    public int Jaulas;
    public Text textoJaulas;

    public int[] JaulasActivas = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, };

    public GameObject[] JaulasArray;

    public TextAsset Cruzas;

    public GameObject barraSaciedad;

    public GameObject feedCount;

    [System.Serializable]
    public class Cruza
    {
        public string id;
        public int popularidad;
    }
    [System.Serializable]
    public class CruzaList
    {
        public Cruza[] cruza;
    }

    public CruzaList myCruzaList = new CruzaList();

    void Awake()
    {
        GetJaulas();
    }

    // Use this for initialization
    void Start () 
    {
        myCruzaList = JsonUtility.FromJson<CruzaList>(Cruzas.text);

        textoJaulas = GameObject.FindGameObjectWithTag("TextoJaulas").GetComponent<Text>();
        Jaulas = PlayerPrefs.GetInt("JaulasOcupadas");

        //GetJaulas();

        int jaulas_ocupadas = PlayerPrefs.GetInt("JaulasOcupadas");

        //print(jaulas_ocupadas);

        //for (int i = 0; i < JaulasArray.Length; i++)
        //{
        //    JaulasArray[i].SetActive(false);
        //}

        //for (int i = 0; i < jaulas_ocupadas; i++)
        //{
        //    JaulasArray[i].SetActive(true);

        //    Sprite cruza_test_img = Resources.Load<Sprite>(PlayerPrefs.GetString("Jaula"+i));

        //    Image Jaula_image = JaulasArray[i].GetComponent<Image>();

        //    Jaula_image.sprite = cruza_test_img;
        //}


        for (int i = 0; i < JaulasActivas.Length; i++)
        {
            JaulasActivas[i] = PlayerPrefs.GetInt("JaulaActiva" + i, JaulasActivas[i]);
            barraSaciedad = GameObject.Find("BarraSaciedad" + i);
            if (barraSaciedad)
            {
                Image barraFill = barraSaciedad.GetComponentInChildren<Image>();

                if (barraFill)
                {
                    if (PlayerPrefs.GetInt("SaciedadJaula" + i) >= 70)
                    {
                        barraFill.color = new Color(0.259f, 0.85f, 0.188f, 1);
                    }
                    else if (PlayerPrefs.GetInt("SaciedadJaula" + i) < 70 && PlayerPrefs.GetInt("SaciedadJaula" + i) >= 40)
                    {
                        barraFill.color = new Color(0.894f, 0.941f, 0.118f, 1);
                    }
                    else if (PlayerPrefs.GetInt("SaciedadJaula" + i) < 40)
                    {
                        barraFill.color = new Color(0.89f, 0.239f, 0.216f, 1);
                    }
                }
            }
            
            feedCount = GameObject.Find("feed" + i);

            if (barraSaciedad)
            {
                GameObject.Find("BarraSaciedad" + i).GetComponent<Slider>().value = PlayerPrefs.GetInt("SaciedadJaula" + i);
            }

            if (feedCount)
            {
                if (JaulasActivas[i] == 1)
                {
                    print("Jaula " + i + " Activa");
                    feedCount.GetComponent<TMP_Text>().text = PlayerPrefs.GetInt("FeedJaula" + i).ToString();
                }
                else
                {
                    print("Jaula " + i + " Inctiva");
                    feedCount.GetComponent<TMP_Text>().text = "0";
                }
            }

        }


        for (int i = 0; i < JaulasActivas.Length; i++)
        {
            if (JaulasActivas[i] == 1)
            {


                JaulasArray[i].SetActive(true);


                Sprite cruza_test_img = Resources.Load<Sprite>(PlayerPrefs.GetString("Jaula" + i));

                Image Jaula_image = JaulasArray[i].GetComponent<Image>();

                Jaula_image.sprite = cruza_test_img;


            }
            else if (JaulasActivas[i] == 0)
            {
                if (i >= 0 && i <= 19)
                {

                    JaulasArray[i].SetActive(false);
                }

            }
        }
    }

    void Update()
    {
        textoJaulas.text = Jaulas.ToString();
        Jaulas = PlayerPrefs.GetInt("JaulasOcupadas");

        for (int i = 0; i < JaulasActivas.Length; i++)
        {
            JaulasActivas[i] = PlayerPrefs.GetInt("JaulaActiva" + i, JaulasActivas[i]);
        }
    }

    GameObject[] GetJaulas()
    {
        JaulasArray = GameObject.FindGameObjectsWithTag("Jaula"); 
        Array.Sort(JaulasArray, CompareJaulas);
        return JaulasArray;
    }

    private int CompareJaulas(GameObject x, GameObject y)
    {
        return Int16.Parse(x.name).CompareTo(Int16.Parse(y.name));
    }

    public void DesocuparJaula(int selectedJaula)
    {
        PlayerPrefs.SetInt("popularidad", PlayerPrefs.GetInt("popularidad") - myCruzaList.cruza[int.Parse(PlayerPrefs.GetString("Jaula" + selectedJaula))].popularidad);


        JaulasArray[selectedJaula].SetActive(false);
        PlayerPrefs.SetInt("JaulaActiva" + selectedJaula, 0);
        PlayerPrefs.SetInt("SaciedadJaula" + selectedJaula, 0);
        PlayerPrefs.SetInt("JaulasOcupadas", PlayerPrefs.GetInt("JaulasOcupadas") - 1);
        PlayerPrefs.SetString("Jaula" + selectedJaula, "");

    }


}
