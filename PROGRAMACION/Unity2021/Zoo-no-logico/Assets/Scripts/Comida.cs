using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comida : MonoBehaviour
{

    public Mati_CruzasAnimales[] cruzas;

    public int selectedJaula;

    // Start is called before the first frame update
    void Start()
    {
        cruzas = Resources.LoadAll<Mati_CruzasAnimales>("");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddSaciedad()
    {
        selectedJaula = PlayerPrefs.GetInt("IndexDesocuparJaula");
        print("Nombre: " + PlayerPrefs.GetString("Jaula" + selectedJaula));
        print("Saciedad antes: " + PlayerPrefs.GetInt("SaciedadJaula" + selectedJaula));
        PlayerPrefs.SetInt("SaciedadJaula" + selectedJaula, PlayerPrefs.GetInt("SaciedadJaula" + selectedJaula) + 10);
        print("Saciedad despues: " + PlayerPrefs.GetInt("SaciedadJaula" + selectedJaula));
    }
}
