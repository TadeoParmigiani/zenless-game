using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisionJoe : MonoBehaviour
{
    // Condicionales para determinadas acciones
    public AceptarMisiones scriptAceptarMisiones;
    public bool misionJoeActiva;
    public bool jugadorCerca;
    public bool ataqueZombie;
    public bool interfazAbierta;
    public bool misionTerminada;
    public bool misionSegundaParte;
    public bool misionJoeTerminada;

    // Zombies
    public GameObject zombie1;
    public GameObject zombie2;
    public GameObject zombie3;
    public GameObject zombie4;
    public GameObject zombie5;
    public GameObject zombie6;
    public GameObject zombie7;
    public GameObject zombie8;
    public GameObject zombie9;
    
    // Dialogos y Objetos
    public GameObject simboloMision;
    public GameObject aceptarMision;
    public GameObject misionAceptada;
    public GameObject zombiesmision1;
    public GameObject zombiesmision2;
    public GameObject misionCompletada;
    public GameObject segundaParteMision;
    public GameObject misionJoe;
    public GameObject joe;


    // Start se llama antes del primer frame
    void Start()
    {
        scriptAceptarMisiones = FindObjectOfType<AceptarMisiones>();
        ataqueZombie = false;
        misionTerminada = false;
        misionSegundaParte = false;
        simboloMision.SetActive(true);
        aceptarMision.SetActive(false);
        zombiesmision1.SetActive(false);
        zombiesmision2.SetActive(false);
        segundaParteMision.SetActive(false);
        //joe.SetActive(false);
    }

    //Update se llama una vez por frame
    void Update()
{
    //Primera Interaccion (Primer Ataque Zombie)
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

    //Segunda Interaccion (Segundo AtaqueZombie)
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

    //Ultima Interaccion (Dialogo Final)
    if (Input.GetKeyDown(KeyCode.F) && jugadorCerca && misionSegundaParte && misionTerminada)
    {
        if (!misionCompletada.activeSelf)
        {
            Pausar();
            misionCompletada.SetActive(true);
            aceptarMision.SetActive(false);

            if (scriptAceptarMisiones != null)
            {
                scriptAceptarMisiones.MisionJoeCompletada = true;
                scriptAceptarMisiones.misionActiva= false;
                scriptAceptarMisiones.IncrementarContadorMisionesCompletadas();
                Debug.Log("¡Misión Joe completada!");
            }
        }
        else
        {
            Volver();
            misionCompletada.SetActive(false);
            misionJoe.SetActive(false);
            joe.SetActive(true);
        }
    }

    //Verificacion de oleada
    VerificarExistenciaZombies();
    VerificarExistenciaZombies2();
}

void VerificarExistenciaZombies()
{
    if (zombie1 == null && zombie2 == null && zombie3 == null && zombie4 == null)
    {
        misionSegundaParte = true;
        ataqueZombie = false;
    }
}

void VerificarExistenciaZombies2()
{
    if (zombie6 == null && zombie7 == null && zombie8 == null && zombie9 == null)
    {
        misionJoeTerminada = true;
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