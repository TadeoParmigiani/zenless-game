using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisionJoe : MonoBehaviour
{
    // Variables relacionadas con el diálogo de Joe
    public GameObject[] secuenciaDialogos1;
    public GameObject[] secuenciaDialogos2;
    public GameObject[] secuenciaDialogos3;
    public GameObject mensajeInteractuar;
    private GameObject[] secuenciaActual;
    private int indiceDialogoActual = 0;
    private bool enDialogo = false;
    private bool primerDialogoCompletado = false;
    private bool segundoDialogoCompletado = false;
    private bool tercerDialogoCompletado = false;
    private bool ataqueZombie = false;
    private bool jugadorCerca = false;

    // Variables relacionadas con la misión de Joe
    public AceptarMisiones scriptAceptarMisiones;
    public GameObject zombiesmision1;
    public GameObject zombiesmision2;
    public GameObject misionJoeTerminada;
    public GameObject Joe;

    // Start se llama antes del primer frame
    void Start()
    {
        scriptAceptarMisiones = FindObjectOfType<AceptarMisiones>();
        zombiesmision1.SetActive(false);
        zombiesmision2.SetActive(false);
        mensajeInteractuar.SetActive(false);
    }


    // Colsion con el collider de Joe
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !ataqueZombie)
        {
            jugadorCerca = true;
            MostrarMensajeInteractivo();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !ataqueZombie)
        {
            jugadorCerca = false;
            OcultarMensajeInteractivo();
        }
    }

    // Update se llama una vez por frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && jugadorCerca && !ataqueZombie && !enDialogo)
        {
            OcultarMensajeInteractivo();
            if (!primerDialogoCompletado)
            {
                IniciarSecuenciaDialogos(secuenciaDialogos1);
                Pausar();
            }
            else if (primerDialogoCompletado && !segundoDialogoCompletado)
            {
                IniciarSecuenciaDialogos(secuenciaDialogos2);
                Pausar();
            }
            else if (primerDialogoCompletado && segundoDialogoCompletado && !tercerDialogoCompletado)
            {
                IniciarSecuenciaDialogos(secuenciaDialogos3);
                Pausar();
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

                if (!primerDialogoCompletado)
                {
                    primerDialogoCompletado = true;
                    ataqueZombie = true;
                    IniciarAtaqueZombie(zombiesmision1);
                }
                else if (primerDialogoCompletado && !segundoDialogoCompletado)
                {
                    segundoDialogoCompletado = true;
                    ataqueZombie = true;
                    StartCoroutine(EsperarYActivarSegundaOleada());
                }
                else if (primerDialogoCompletado && segundoDialogoCompletado && !tercerDialogoCompletado)
                {
                    tercerDialogoCompletado = true;
                    scriptAceptarMisiones.MisionJoeCompletada = true;
                    scriptAceptarMisiones.misionActiva= false;
                    scriptAceptarMisiones.IncrementarContadorMisionesCompletadas();
                    Debug.Log("¡Mision Joe completada!");
                    misionJoeTerminada.SetActive(false);
                    Joe.SetActive(true);
                }
            }
        }

        // Verificar si todos los zombies de la primera oleada han sido derrotados
        if (ataqueZombie && zombiesmision1.activeSelf && TodosZombiesMuertos(zombiesmision1))
        {
            ataqueZombie = false;
        }

        // Verificar si todos los zombies de la segunda oleada han sido derrotados
        if (ataqueZombie && zombiesmision2.activeSelf && TodosZombiesMuertos(zombiesmision2))
        {
            ataqueZombie = false;
        }
    }

    //Funciones de Dialogos
    void IniciarSecuenciaDialogos(GameObject[] secuencia)
    {
        indiceDialogoActual = 0;
        enDialogo = true;
        secuenciaActual = secuencia;
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

    //Zombies
    void IniciarAtaqueZombie(GameObject oleadaZombie)
    {
        oleadaZombie.SetActive(true);
    }

    bool TodosZombiesMuertos(GameObject zombies)
    {
        foreach (Transform zombie in zombies.transform)
        {
            if (zombie.gameObject.activeSelf)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator EsperarYActivarSegundaOleada()
    {
        yield return new WaitForSeconds(5f); // Esperar 30 segundos
        IniciarAtaqueZombie(zombiesmision2); // Activar la segunda oleada de zombies
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