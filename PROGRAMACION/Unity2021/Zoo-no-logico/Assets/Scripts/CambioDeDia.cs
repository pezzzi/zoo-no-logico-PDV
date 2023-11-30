﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CambioDeDia : MonoBehaviour {


    public Text textoTurno;
    public int numTurno;
    public GameObject Pantalla;
    public GameObject PantallaPostEvento;
    public GameObject PantallaPerder;
    public GameObject PantallaGanar;
    public GameObject PantallaAnimalFallecido;
    public int Popularidad;
    public GameObject PopularidadBarra;
    private string[] listaAnimales = new string[] { "Carpincho", "Cocodrilo", "Arana", "Ave", "Serpiente", "Zorro", "Murcielago" };
    private string tempGO;

    public GameObject aranaDesbloqueada;
    public GameObject aveDesbloqueada;
    public GameObject serpienteDesbloqueada;

    public float speed = 10.0f;

    [SerializeField] private TextAsset Cruzas;

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


    [SerializeField] private GameObject ANALYTICS;

    [SerializeField] private Saciedad saciedadCtrl;

    // Use this for initialization
    void Start() {
        textoTurno = GameObject.FindGameObjectWithTag("TextoDias").GetComponent<Text>();
        ANALYTICS = GameObject.FindGameObjectWithTag("ANALYTICS");
    }

    // Update is called once per frame
    void Update() {
        numTurno = PlayerPrefs.GetInt("Dias");
        textoTurno.text = "DIA: " + numTurno.ToString();
        Popularidad = PlayerPrefs.GetInt("Popularidad");

        if (Popularidad > 25 && PlayerPrefs.GetInt("aranaDesbloqueada") == 0)
        {
            PlayerPrefs.SetInt("aranaDesbloqueada", 1);
            PlayerPrefs.SetInt("CantidadArana", PlayerPrefs.GetInt("CantidadArana") + 1);
            print(aranaDesbloqueada.transform.position);
            aranaDesbloqueada.SetActive(true);
           
            StartCoroutine(DestruirObjeto(aranaDesbloqueada));
            print(aranaDesbloqueada.transform.position);
        }
        if (Popularidad > 30 && PlayerPrefs.GetInt("aveDesbloqueada") == 0)
        {
            PlayerPrefs.SetInt("aveDesbloqueada", 1);
            PlayerPrefs.SetInt("CantidadAve", PlayerPrefs.GetInt("CantidadAve") + 1);
            //DestruirObjeto(aveDesbloqueada);
            StartCoroutine(DestruirObjeto(aveDesbloqueada));
        }
        if (Popularidad > 35 && PlayerPrefs.GetInt("serpienteDesbloqueada") == 0)
        {
            PlayerPrefs.SetInt("serpienteDesbloqueada", 1);
            PlayerPrefs.SetInt("CantidadSerpiente", PlayerPrefs.GetInt("CantidadSerpiente") + 1);
            //DestruirObjeto(serpienteDesbloqueada);
            StartCoroutine(DestruirObjeto(serpienteDesbloqueada));
        }

        PlayerPrefs.SetString("Slot1", "");
        PlayerPrefs.SetString("Slot2", "");
        PlayerPrefs.SetString("Slot3", "");
    }


    public void Pasar()
    {
        Debug.Log("Nuevo dia");
        myCruzaList = JsonUtility.FromJson<CruzaList>(Cruzas.text);
        Debug.Log(myCruzaList + " Cruza list");
        print("Test: " + myCruzaList.cruza[0].popularidad);
        if (!PantallaPostEvento)
        {
            numTurno++;
            textoTurno.text = "DIA: " + numTurno.ToString();
            Pantalla.SetActive(false);
            PlayerPrefs.SetInt("Dias", numTurno);
            PopularidadBarra.SetActive(true);
            PlayerPrefs.SetInt("EventoCartas", 1);
            for (int i = 0; i < 20; i++)
            {
                int feedCount = PlayerPrefs.GetInt("FeedJaula" + i);
                if (PlayerPrefs.GetInt("SaciedadJaula" + i) > 0 && feedCount > 0)
                {
                    PlayerPrefs.SetInt("alimentarAnimalTotal", PlayerPrefs.GetInt("alimentarAnimalTotal") + 1);
                    ANALYTICS.SendMessage("alimentar", i);
                    for (int e = 0; e < feedCount; e++)
                    {
                        saciedadCtrl.AddSaciedadByJaula(i, feedCount);
                        PlayerPrefs.SetInt("FeedJaula" + i, 0);
                    }
                }

                PlayerPrefs.SetInt("SaciedadJaula" + i, PlayerPrefs.GetInt("SaciedadJaula" + i) - 10);
                if ((PlayerPrefs.GetInt("JaulaActiva" + i) == 1) && PlayerPrefs.GetInt("SaciedadJaula" + i) <= 0)
                {
                    PlayerPrefs.SetInt("animalMuertoTotal", PlayerPrefs.GetInt("animalMuertoTotal") + 1);
                    ANALYTICS.SendMessage("animal_fallecido", i);

                    print(int.Parse(PlayerPrefs.GetString("Jaula" + i)));
                    PlayerPrefs.SetInt("popularidad", PlayerPrefs.GetInt("popularidad") - myCruzaList.cruza[int.Parse(PlayerPrefs.GetString("Jaula" + i))].popularidad);

                    PlayerPrefs.SetInt("JaulaActiva" + i, 0);
                    PlayerPrefs.SetInt("SaciedadJaula" + i, 0);
                    PlayerPrefs.SetInt("JaulasOcupadas", PlayerPrefs.GetInt("JaulasOcupadas") - 1);
                    PlayerPrefs.SetString("Jaula" + i, "");

                    PantallaAnimalFallecido.SetActive(true);
                }

                //PlayerPrefs.GetString("Jaula" + i);

            }
        }
        else
        {
            Pantalla.SetActive(false);
            PlayerPrefs.SetInt("ImpuestoXDiasSinCruzas", PlayerPrefs.GetInt("ImpuestoXDiasSinCruzas"));
        }
    }

    public void CerrarPantallaAnimalFallecido()
    {
        PantallaAnimalFallecido.SetActive(false);
    }

    public void AbrirPantalla()
    {

        int Random1= new System.Random().Next(0, 20);
        int Random2 = new System.Random().Next(0, 20);
        int Random3 = new System.Random().Next(1, 13);
        //listaAnimales
        print(listaAnimales[0]);
        for (int i = 0; i < listaAnimales.Length; i++)
        {
            int rnd = Random.Range(0, listaAnimales.Length);
            tempGO = listaAnimales[rnd];
            listaAnimales[rnd] = listaAnimales[i];
            listaAnimales[i] = tempGO;
        }
        print ("RANDOM ACA: " + listaAnimales[0]);

        PlayerPrefs.SetString("animal1Tienda", listaAnimales[0]);
        PlayerPrefs.SetString("animal2Tienda", listaAnimales[1]);
        PlayerPrefs.SetString("animal3Tienda", listaAnimales[2]);
        PlayerPrefs.SetString("animal4Tienda", listaAnimales[3]);
        PlayerPrefs.SetString("animal5Tienda", listaAnimales[4]);
        PlayerPrefs.SetString("animal6Tienda", listaAnimales[5]);
        PlayerPrefs.SetString("animal7Tienda", listaAnimales[6]);

        print(PlayerPrefs.GetString("animal7Tienda"));

        PlayerPrefs.SetInt("comentarioRandom1", Random1);
        PlayerPrefs.SetInt("comentarioRandom2", Random2);
        PlayerPrefs.SetInt("avatarRandom", Random3);

        PlayerPrefs.SetInt("ImpuestoXDiasSinCruzas", PlayerPrefs.GetInt("ImpuestoXDiasSinCruzas")+1);

        if (Popularidad >= 100 && PlayerPrefs.GetInt("Ganaste")==0)
        {
            PantallaGanar.SetActive(true);
            PlayerPrefs.SetInt("Ganaste", 1);
            PlayerPrefs.SetInt("ActivadorCalificacion", 1);
            ANALYTICS.SendMessage("ganar");
        }
        else if (Popularidad <= 0 && PlayerPrefs.GetInt("Ganaste") == 0)
        {
            PantallaPerder.SetActive(true);
            ANALYTICS.SendMessage("game_over");
        }
        else 
        {
            Pantalla.SetActive(true);
            PopularidadBarra.SetActive(false);
        }


        if (PlayerPrefs.GetInt("Ganaste") == 1)
        {
            PlayerPrefs.SetInt("CantidadCarpincho", 99);
            PlayerPrefs.SetInt("CantidadArana", 99);
            PlayerPrefs.SetInt("CantidadAve", 99);
            PlayerPrefs.SetInt("CantidadZorro", 99);
            PlayerPrefs.SetInt("CantidadCocodrilo", 99);
            PlayerPrefs.SetInt("CantidadSerpiente", 99);
            PlayerPrefs.SetInt("CantidadMurcielago", 99);
            PlayerPrefs.SetInt("Moneditas", 999999999);
        }

        ANALYTICS.SendMessage("fin_del_turno");


    }

    public void OnPantallaPostEvento()
    {
        if (PantallaPostEvento)
        {
            PantallaPostEvento.SetActive(true);
        }
    }


    private IEnumerator DestruirObjeto(GameObject objeto)
    {
        //aranaDesbloqueada.transform.position = new Vector3(aranaDesbloqueada.transform.position.x - speed * Time.deltaTime, aranaDesbloqueada.transform.position.y, aranaDesbloqueada.transform.position.z);
        objeto.SetActive(true);
        yield return new WaitForSeconds(3);
        objeto.SetActive(false);
        Destroy(aranaDesbloqueada);
    }
}


