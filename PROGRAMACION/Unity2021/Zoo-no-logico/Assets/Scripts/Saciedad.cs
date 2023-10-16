using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Saciedad : MonoBehaviour
{

    public Mati_CruzasAnimales[] cruzas;

    public int selectedJaula;

    public int saciedad;

    public Slider slider;

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
        selectedJaula = PlayerPrefs.GetInt("IndexDesocuparJaula");
        saciedad = PlayerPrefs.GetInt("SaciedadJaula" + selectedJaula);
        if (saciedad < 100)
        {
            print("Nombre: " + PlayerPrefs.GetString("Jaula" + selectedJaula));
            print("Saciedad antes: " + saciedad);
            PlayerPrefs.SetInt("SaciedadJaula" + selectedJaula, saciedad + 10);
            slider = GameObject.Find("BarraSaciedad" + selectedJaula).GetComponent<Slider>();
            Debug.Log(slider.value);
            SetSaciedad(selectedJaula);
            print("Saciedad despues: " + saciedad);
        }
    }
}
