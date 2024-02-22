using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisionInvestigador : MonoBehaviour
{
    // Variables relacionadas con el diálogo del Investigador
    public GameObject[] secuenciaDialogos1;
    public GameObject[] secuenciaDialogos2;
    public GameObject[] secuenciaDialogos3;
    public GameObject mensajeInteractuar;
    private GameObject[] secuenciaActual;
    private int indiceDialogoActual = 0;
    private bool enDialogo = false;
    private bool primerDialogoCompletado = false;
    private bool jugadorCerca = false;

    // Variables relacionadas con la misión del Investigador
    public AceptarMisiones scriptAceptarMisiones;
    public valorvidaP scriptvalorvidaP;
    public int cantidadCorazonesARestar = 3; //Asignar la misma cantidad de corazones que pide el investigador
    public bool requisitosCompletados = false;
    public GameObject misionInvestigadorTerminada;
    public GameObject investigador;
    public GameObject investigadorMision;

    // Start is called before the first frame update
    void Start()
    {
        scriptAceptarMisiones = FindObjectOfType<AceptarMisiones>();
        scriptvalorvidaP = FindObjectOfType<valorvidaP>(); // Cambiado a valorvidaP
        mensajeInteractuar.SetActive(false);
        investigador.SetActive(false);
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

    void IniciarSecuenciaDialogos(GameObject[] secuencia)
    {
        indiceDialogoActual = 0;
        enDialogo = true;
        secuenciaActual = secuencia;
        Pausar();
        MostrarDialogoActual();
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && jugadorCerca && !enDialogo && !primerDialogoCompletado)
        {
            OcultarMensajeInteractivo();
            IniciarSecuenciaDialogos(secuenciaDialogos1);
            primerDialogoCompletado = true;
        }

        if (Input.GetKeyDown(KeyCode.F) && jugadorCerca && !enDialogo && primerDialogoCompletado)
        {
            if (scriptvalorvidaP.corazones >= 3) 
            {
                OcultarMensajeInteractivo();
                IniciarSecuenciaDialogos(secuenciaDialogos3);
                requisitosCompletados = true;
                scriptvalorvidaP.corazones -= cantidadCorazonesARestar;
            }
            else
            {
                OcultarMensajeInteractivo();
                IniciarSecuenciaDialogos(secuenciaDialogos2);
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

                if (requisitosCompletados)
                {
                    scriptAceptarMisiones.MisionInvestigadorCompletada = true;
                    scriptAceptarMisiones.misionActiva = false;
                    scriptAceptarMisiones.IncrementarContadorMisionesCompletadas();
                    Debug.Log("¡Misión Investigador completada!");
                    misionInvestigadorTerminada.SetActive(false);
                    investigador.SetActive(true);
                }
            }
        }
    }

    // Tiempo
    void Volver()
    {
        Time.timeScale = 1f;
    }

    void Pausar()
    {
        Time.timeScale = 0f;
    }
}

