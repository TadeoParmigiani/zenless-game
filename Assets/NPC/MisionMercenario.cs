using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisionMercenario : MonoBehaviour
{
    public bool misionActiva;
    public bool jugadorCerca;
    public bool ataqueZombie;
    public bool interfazAbierta;
    public bool misionTerminada;
    public bool misionSegundaParte;
    //zombies 
    public GameObject zombie1;
    public GameObject zombie2;
    public GameObject zombie3;
    public GameObject zombie4;
    public GameObject zombie5;
    public GameObject zombie6;
    public GameObject zombie7;
    public GameObject zombie8;
    public GameObject zombie9;
    //
    public GameObject simboloMision;
    public GameObject aceptarMision;
    public GameObject misionAceptada;
    public GameObject zombiesmision1;
    public GameObject zombiesmision2;
    public GameObject misionCompletada;
    public GameObject segundaParteMision;
    public GameObject simboloDeMision;
    public GameObject misionMercenario;
    public GameObject mercenario;


    // Start se llama antes del primer frame
    void Start()
    {
        ataqueZombie = false;
        misionActiva = false;
        misionTerminada = false;
        misionSegundaParte = false;
        simboloMision.SetActive(true);
        aceptarMision.SetActive(false);
        zombiesmision1.SetActive(false);
        zombiesmision2.SetActive(false);
        misionCompletada.SetActive(false);
        segundaParteMision.SetActive(false);
        //mercenario.SetActive(false);
    }

    //Update se llama una vez por frame
void Update()
{
    //Primera Interaccion
    if (Input.GetKeyDown(KeyCode.F) && jugadorCerca && !misionSegundaParte)
    {
        if (!misionAceptada.activeSelf && ataqueZombie == false)
        {
            Pausar();
            aceptarMision.SetActive(false);
            misionAceptada.SetActive(true);
        }    
        else if (Input.GetKeyDown(KeyCode.F))
        {
            Volver();
            misionAceptada.SetActive(false);
            zombiesmision1.SetActive(true);
            ataqueZombie = true;
        }               
    }

    //Segunda Interaccion
    if (Input.GetKeyDown(KeyCode.F) && jugadorCerca && misionSegundaParte && !misionTerminada)
    {
        if (!segundaParteMision.activeSelf)
        {
            Pausar();
            segundaParteMision.SetActive(true);
            aceptarMision.SetActive(false);
        }
        else
        {
            Volver();
            segundaParteMision.SetActive(false);
            zombiesmision2.SetActive(true);
            ataqueZombie = true;
        }
    }

    //Ultima Interaccion 
    if (Input.GetKeyDown(KeyCode.F) && jugadorCerca && misionSegundaParte && misionTerminada)
    {
        if (!misionCompletada.activeSelf)
        {
            Pausar();
            misionCompletada.SetActive(true);
            aceptarMision.SetActive(false);
        }
        else
        {
            Volver();
            misionCompletada.SetActive(false);
            misionMercenario.SetActive(false);
            mercenario.SetActive(true);
        }
    }

    //Verificacion de oleada
    VerificarExistenciaZombies();
    VerificarExistenciaZombies2();
}

void VerificarExistenciaZombies()
{
    if (zombie1 == null && zombie2 == null && zombie3 == null && zombie4 == null && zombie5 == null)
    {
        misionSegundaParte = true;
        ataqueZombie = false;
    }
}

void VerificarExistenciaZombies2()
{
    if (zombie6 == null && zombie7 == null && zombie8 == null && zombie9 == null)
    {
        misionTerminada = true;
        ataqueZombie = false;
    }
}

void Volver()
{
    Time.timeScale = 1f;
}

void Pausar()
{
    Time.timeScale = 0f;
}
    
void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Player"))
    {
        jugadorCerca = true;
        if (!interfazAbierta && ataqueZombie == false)
        {
            aceptarMision.SetActive(true);
        }
    }
}

void OnTriggerExit(Collider other)
{
    if (other.CompareTag("Player"))
    {
        jugadorCerca = false;
        aceptarMision.SetActive(false);
    }
}

}