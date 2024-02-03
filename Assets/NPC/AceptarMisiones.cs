using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AceptarMisiones : MonoBehaviour
{
    //Contador de Misiones
    public int misionesCompletadasCantidad = 0;
    public bool elegirMisionPorContador = false;
    
    // Misiones Completadas
    public bool MisionMercenarioCompletada;
    public bool MisionJoeCompletada;
    public bool MisionGeneradorCompletada;
    public bool MisionInvestigadorCompletada;

    // Interaccion con Alcalde (Cercania)
    public bool jugadorCerca;
    public bool interfazAbierta;
    public bool misionActiva;
    public GameObject misionSolicitud;

    //Interaccion con Alcalde (Mercenario)
    public GameObject misionMercenarioSolicitud;
    public GameObject misionMercenario;

    //Interaccion con Alcalde (Joe)
    public GameObject misionJoeSolicitud;
    public GameObject misionJoe;

    //Interaccion con Alcalde (Generador)
    public GameObject misionGeneradorSolicitud;
    public GameObject misionGenerador;

    //Interaccion con Alcalde (Investigador)
    public GameObject misionInvestigadorSolicitud;
    public GameObject misionInvestigador;

void Start()
{
    misionActiva = false;
    elegirMisionPorContador = false;
    misionSolicitud.SetActive(false);
    misionMercenario.SetActive(false);
    misionJoeSolicitud.SetActive(false);
    misionGeneradorSolicitud.SetActive(false);
    misionMercenarioSolicitud.SetActive(false);
    misionInvestigadorSolicitud.SetActive(false);
}

void Update()
{
    if (Input.GetKeyDown(KeyCode.F) && jugadorCerca && !misionActiva)
    {
        if (!misionMercenarioSolicitud.activeSelf && misionesCompletadasCantidad == 0)
        {
            Pausar();
            misionSolicitud.SetActive(false);
            misionMercenarioSolicitud.SetActive(true);
            elegirMisionPorContador = true;
        }    
        else if (!misionJoeSolicitud.activeSelf && misionesCompletadasCantidad == 1)
        {
            Pausar();
            misionSolicitud.SetActive(false);
            misionJoeSolicitud.SetActive(true);
            elegirMisionPorContador = true;
        }    
        else if (!misionGeneradorSolicitud.activeSelf && misionesCompletadasCantidad == 2)
        {
            Pausar();
            misionSolicitud.SetActive(false);
            misionGeneradorSolicitud.SetActive(true);
            elegirMisionPorContador = true;
        }    
        else if (!misionInvestigadorSolicitud.activeSelf && misionesCompletadasCantidad == 3)
        {
            Pausar();
            misionSolicitud.SetActive(false);
            misionInvestigadorSolicitud.SetActive(true);
            elegirMisionPorContador = true;
        }
    }
    else if (Input.GetKeyDown(KeyCode.F) && elegirMisionPorContador)
    {
        Volver();
        misionJoeSolicitud.SetActive(false);
        misionGeneradorSolicitud.SetActive(false);
        misionMercenarioSolicitud.SetActive(false);
        misionInvestigadorSolicitud.SetActive(false);

        // Activa las misiones correspondientes
        if (misionesCompletadasCantidad == 0)
        {
            misionMercenario.SetActive(true);
        }
        else if (misionesCompletadasCantidad == 1)
        {
            misionJoe.SetActive(true);
        }
        else if (misionesCompletadasCantidad == 2)
        {
            misionGenerador.SetActive(true);
        }
        else if (misionesCompletadasCantidad == 3)
        {
            misionInvestigador.SetActive(true);
        }
        
        misionActiva = true;
        elegirMisionPorContador = false;
    }
}

    
void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Player"))
    {
        jugadorCerca = true;
        {
            misionSolicitud.SetActive(true);
        }
    }
}

void OnTriggerExit(Collider other)
{
    if (other.CompareTag("Player"))
    {
        jugadorCerca = false;
        misionSolicitud.SetActive(false);
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

public void IncrementarContadorMisionesCompletadas()
{
    misionesCompletadasCantidad++;
}

}