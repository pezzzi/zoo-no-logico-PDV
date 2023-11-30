using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class CageDetailConstructor : MonoBehaviour
{
    public TextAsset Cruzas;
    public Text plata;
    public Text popularidad;
    public Image animal;
    public GameObject[] CruzasArray;
    public GameObject[] JaulasArray;

    public Slider slider;
    public Image sliderFill;

    [System.Serializable]
    public class Cruza
    {
        public string id;
        public string nombre;
        public string descripcion;
        public string dato1;
        public string dato2;
        public string dato3;
        public int porcentaje;
        public int popularidad;
        public int peligrosidad;
        public int dinero;
    }

    [System.Serializable]
    public class CruzaList
    {
        public Cruza[] cruza;
    }

    public CruzaList myCruzaList = new CruzaList();

    void Awake()
    {
        //PlayerPrefs.SetInt("totalCodex", 0);
        GetJaulas();
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        myCruzaList = JsonUtility.FromJson<CruzaList>(Cruzas.text);
        int index = PlayerPrefs.GetInt("IndexDesocuparJaula");
        Debug.Log("Index detalle: " + index);

        Sprite cruza_test_img = Resources.Load<Sprite>(index.ToString());
        print("Jaula arrray length: " + JaulasArray.Length);
        //Image Cruza_image = CruzasArray[index].GetComponent<Image>();
        for (int i = 0; i < JaulasArray.Length; i++)
        {
            if (JaulasArray[i].name == index.ToString())
            {
                animal.sprite = JaulasArray[i].GetComponent<Image>().sprite;
            }
        }

        //animal.sprite = JaulasArray[index].GetComponent<Image>().sprite;
        int cageDivisor;
        if (PlayerPrefs.GetInt("cageDivisor") > 0)
        {
            cageDivisor = PlayerPrefs.GetInt("cageDivisor");
        }
        else
        {
            cageDivisor = 2;
        }
        //print(PlayerPrefs.GetInt("cage" + index.ToString() + "Money"));
        if (PlayerPrefs.GetInt("SaciedadJaula" + index.ToString()) >= 70)
        {
            int dineroCruza = myCruzaList.cruza[int.Parse(PlayerPrefs.GetString("Jaula" + index.ToString()))].dinero;
            int plataInt = (int)(dineroCruza / (cageDivisor - 1f));
            plata.text = plataInt.ToString();

            sliderFill.color = new Color(0.259f, 0.85f, 0.188f, 1);

        }
        else if (PlayerPrefs.GetInt("SaciedadJaula" + index.ToString()) < 70 && PlayerPrefs.GetInt("SaciedadJaula" + index.ToString()) >= 40)
        {
            int dineroCruza = myCruzaList.cruza[int.Parse(PlayerPrefs.GetString("Jaula" + index.ToString()))].dinero;
            int plataInt = (int)(dineroCruza / cageDivisor);
            plata.text = plataInt.ToString();

            sliderFill.color = new Color(0.894f, 0.941f, 0.118f, 1);
        }
        else if (PlayerPrefs.GetInt("SaciedadJaula" + index.ToString()) < 40)
        {
            int dineroCruza = myCruzaList.cruza[int.Parse(PlayerPrefs.GetString("Jaula" + index.ToString()))].dinero;
            int plataInt = (int)(dineroCruza / (cageDivisor + 1f));
            plata.text = plataInt.ToString();

            sliderFill.color = new Color(0.89f, 0.239f, 0.216f, 1);
        }
        slider.value = PlayerPrefs.GetInt("SaciedadJaula" + index.ToString());

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
}
