using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AceptarMisiones : MonoBehaviour
{
    //Interaccion con Alcalde (Mercenario)
    public GameObject[] secuenciaDialogosMercenario;
    public GameObject misionMercenario;
    public GameObject Mercenario;

    //Interaccion con Alcalde (Joe)
    public GameObject[] secuenciaDialogosJoe;
    public GameObject misionJoe;
    public GameObject Joe;

    //Interaccion con Alcalde (Generador)
    public GameObject[] secuenciaDialogosGenerador;
    public GameObject misionGenerador;

    //Interaccion con Alcalde (Investigador)
    public GameObject[] secuenciaDialogosInvestigador;
    public GameObject misionInvestigador;
    public GameObject Investigador;

    //Contador de Misiones
    public int misionesCompletadasCantidad = 0;
    public bool elegirMisionPorContador = false;
    
    // Misiones Completadas
    public bool MisionMercenarioCompletada;
    public bool MisionJoeCompletada;
    public bool MisionGeneradorCompletada;
    public bool MisionInvestigadorCompletada;

    // Interaccion con Alcalde (Cercania)
    public GameObject mensajeInteractuar;
    private GameObject[] secuenciaActual;
    private int indiceDialogoActual = 0;
    private bool enDialogo = false;
    public bool jugadorCerca;
    public bool interfazAbierta;
    public bool misionActiva;

    void Start()
    {
        //Dialogo alcalde
        misionActiva = false;
        elegirMisionPorContador = false;

        //Misiones
        misionMercenario.SetActive(false);
        misionJoe.SetActive(false);
        misionGenerador.SetActive(false);
        misionInvestigador.SetActive(false);

        //NPC
        Mercenario.SetActive(false);
        Joe.SetActive(false);
        Investigador.SetActive(false);
    }

    void MostrarDialogoActual()
        {
            secuenciaActual[indiceDialogoActual].SetActive(true);
        }

    void OcultarDialogoActual()
    {
        secuenciaActual[indiceDialogoActual].SetActive(false);
    }

    void MostrarMensajeInteractivo()
    {
        mensajeInteractuar.SetActive(true);
    }

    void OcultarMensajeInteractivo()
    {
        mensajeInteractuar.SetActive(false);
    }

    void IniciarSecuenciaDialogos(GameObject[] secuencia)
    {
        indiceDialogoActual = 0;
        enDialogo = true;
        secuenciaActual = secuencia;
        Pausar();
        MostrarDialogoActual();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && jugadorCerca && !misionActiva && !enDialogo)
        {
            if (misionesCompletadasCantidad == 0)
            {
                OcultarMensajeInteractivo();
                IniciarSecuenciaDialogos(secuenciaDialogosMercenario);
                misionMercenario.SetActive(true);
            }    
            else if (misionesCompletadasCantidad == 1)
            {
                OcultarMensajeInteractivo();
                IniciarSecuenciaDialogos(secuenciaDialogosJoe);
                misionJoe.SetActive(true);
            }    
            else if (misionesCompletadasCantidad == 2)
            {
                OcultarMensajeInteractivo();
                IniciarSecuenciaDialogos(secuenciaDialogosGenerador);
                misionGenerador.SetActive(true);
            }    
            else if (misionesCompletadasCantidad == 3)
            {
                OcultarMensajeInteractivo();
                IniciarSecuenciaDialogos(secuenciaDialogosInvestigador);
                misionInvestigador.SetActive(true);
            }
        }
        
        if (enDialogo && Input.GetKeyDown(KeyCode.F))
        {
            OcultarDialogoActual();

            if (indiceDialogoActual < secuenciaActual.Length - 1)
            {
                indiceDialogoActual++;
                MostrarDialogoActual();
            }
            else
            {
                enDialogo = false;
                Volver();
            }
        }        
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true;
            MostrarMensajeInteractivo();
            
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;
            OcultarMensajeInteractivo();
            
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