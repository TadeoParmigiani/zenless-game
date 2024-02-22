using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MisionGenerador : MonoBehaviour
{
    // Variables relacionadas con el diálogo de Mision Generador
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

    // Variables relacionadas con la misión de Generador
    public AceptarMisiones scriptAceptarMisiones;
    public ThirdPersonControllerMovement scriptMovimiento;
    public GameObject zombiesmision1;
    public GameObject zombiesmision2;
    public GameObject misionGeneradorTerminada;
    public GameObject luz;
    public bool LuzActiva = false;

    // Variables para el sistema de reparación
    public Slider barraProgreso;
    private bool primergeneradorReparado = false;
    private bool segundogeneradorReparado = false;
    private bool tercergeneradorReparado = false;
    private float tiempoTeclaPresionada = 3f; // Tiempo necesario para mantener presionada la tecla F
    private float tiempoRestantePresionado = 0f;

    // Start se llama antes del primer frame
    void Start()
    {
        scriptAceptarMisiones = FindObjectOfType<AceptarMisiones>();
        scriptMovimiento = FindObjectOfType<ThirdPersonControllerMovement>();
        zombiesmision1.SetActive(false);
        zombiesmision2.SetActive(false);
        mensajeInteractuar.SetActive(false);
        barraProgreso.gameObject.SetActive(false);
    }
    

    // Colsion con el collider del Generador
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
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;
            OcultarMensajeInteractivo();
        }
    } 

    // Funciones de Dialogo
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

    // Zombies
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

    IEnumerator EsperarYActivarOleada()
    {
        yield return new WaitForSeconds(2f); // Esperar 30 segundos
        IniciarAtaqueZombie(zombiesmision1);
    }

    IEnumerator EsperarYActivarOleada2()
    {
        yield return new WaitForSeconds(2f); // Esperar 30 segundos
        IniciarAtaqueZombie(zombiesmision2);
    }

    // Update is called once per frame
    void Update()
    {
        // Primera Reparacion de Generador

        if (jugadorCerca && !ataqueZombie && !enDialogo && !primergeneradorReparado && !segundogeneradorReparado)
        {
            if (Input.GetKeyDown(KeyCode.F)) // Verificar si se presiona la tecla F
            {
                OcultarMensajeInteractivo();
                Debug.Log("Reparando el generador");
                tiempoRestantePresionado = tiempoTeclaPresionada; // Reiniciar el tiempo restante
                PausarTPC(); // Pausar al personaje
                barraProgreso.gameObject.SetActive(true);
            }

            if (Input.GetKey(KeyCode.F)) // Verificar si se mantiene presionada la tecla F
            {
                tiempoRestantePresionado -= Time.deltaTime;
                barraProgreso.value = 1 - (tiempoRestantePresionado / tiempoTeclaPresionada);
                if (tiempoRestantePresionado <= 0f)
                {
                    barraProgreso.gameObject.SetActive(false);
                    primergeneradorReparado = true;
                    ataqueZombie = true;
                    Debug.Log("Generador Reparado");
                    luz.SetActive(true);
                    IniciarSecuenciaDialogos(secuenciaDialogos1);
                    VolverTPC();
                }
            }

            else if (Input.GetKeyUp(KeyCode.F)) // Verificar si se suelta la tecla F
            {
                barraProgreso.gameObject.SetActive(false);
                MostrarMensajeInteractivo();
                Debug.Log("Reparacion del generador cancelada");
                tiempoRestantePresionado = tiempoTeclaPresionada; // Reiniciar el tiempo restante
                VolverTPC(); // Volver a activar el movimiento del personaje
            }  
        }

        // Segunda Reparacion de Generador

        if (jugadorCerca && !ataqueZombie && !enDialogo && primergeneradorReparado && !segundogeneradorReparado)    
        {
            if (Input.GetKeyDown(KeyCode.F)) // Verificar si se presiona la tecla F
            {
                OcultarMensajeInteractivo();
                Debug.Log("Reparando el generador");
                tiempoRestantePresionado = tiempoTeclaPresionada; // Reiniciar el tiempo restante
                PausarTPC(); // Pausar al personaje
                barraProgreso.gameObject.SetActive(true);
            }

            if (Input.GetKey(KeyCode.F)) // Verificar si se mantiene presionada la tecla F
            {
                tiempoRestantePresionado -= Time.deltaTime;
                barraProgreso.value = 1 - (tiempoRestantePresionado / tiempoTeclaPresionada);
                if (tiempoRestantePresionado <= 0f)
                {
                    barraProgreso.gameObject.SetActive(false);
                    segundogeneradorReparado = true;
                    ataqueZombie = true;
                    Debug.Log("Generador 2 Reparado ");
                    luz.SetActive(true);
                    tiempoRestantePresionado -= Time.deltaTime;
                    IniciarSecuenciaDialogos(secuenciaDialogos2);
                    VolverTPC();
                    //tiempoRestantePresionado = tiempoTeclaPresionada;                    
                }
            }

            else if (Input.GetKeyUp(KeyCode.F)) // Verificar si se suelta la tecla F
            {
                barraProgreso.gameObject.SetActive(false);
                MostrarMensajeInteractivo();
                Debug.Log("Reparacion del generador cancelada");
                tiempoRestantePresionado = tiempoTeclaPresionada; // Reiniciar el tiempo restante
                VolverTPC(); // Volver a activar el movimiento del personaje
            }
        }

        // Tercera Reparacion de Generador

        if (jugadorCerca && !ataqueZombie && !enDialogo && primergeneradorReparado && segundogeneradorReparado)
        {
            if (Input.GetKeyDown(KeyCode.F)) // Verificar si se presiona la tecla F
            {
                tiempoRestantePresionado = tiempoTeclaPresionada;
                OcultarMensajeInteractivo();
                Debug.Log("Reparando el generador");
                tiempoRestantePresionado = tiempoTeclaPresionada; // Reiniciar el tiempo restante
                PausarTPC(); // Pausar al personaje
                barraProgreso.gameObject.SetActive(true);
            }

            if (Input.GetKey(KeyCode.F)) // Verificar si se mantiene presionada la tecla F
            {
                tiempoRestantePresionado -= Time.deltaTime;
                barraProgreso.value = 1 - (tiempoRestantePresionado / tiempoTeclaPresionada);
                if (tiempoRestantePresionado <= 0f)
                {
                    barraProgreso.gameObject.SetActive(false);
                    tercergeneradorReparado = true;
                    ataqueZombie = true;
                    luz.SetActive(true);
                    Debug.Log("Generador 3 Reparado");
                    IniciarSecuenciaDialogos(secuenciaDialogos3);
                    VolverTPC();
                }
            }

            else if (Input.GetKeyUp(KeyCode.F)) // Verificar si se suelta la tecla F
            {
                barraProgreso.gameObject.SetActive(false);
                MostrarMensajeInteractivo();
                Debug.Log("Reparacion del generador cancelada");
                tiempoRestantePresionado = tiempoTeclaPresionada; // Reiniciar el tiempo restante
                VolverTPC(); // Volver a activar el movimiento del personaje
            }  
        }

        //Acciones Dentro del Dialogo

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

                if (primergeneradorReparado && !primerDialogoCompletado)
                {
                    primerDialogoCompletado = true;
                    StartCoroutine(EsperarYActivarOleada());
                }
                else if (segundogeneradorReparado && !segundoDialogoCompletado)
                {
                    segundoDialogoCompletado = true;
                    StartCoroutine(EsperarYActivarOleada2());
                }
                else if (tercergeneradorReparado && !tercerDialogoCompletado)
                {
                    tercerDialogoCompletado = true;
                    scriptAceptarMisiones.MisionJoeCompletada = true;
                    scriptAceptarMisiones.misionActiva= false;
                    scriptAceptarMisiones.IncrementarContadorMisionesCompletadas();
                    Debug.Log("¡Mision Generador completada!");
                    misionGeneradorTerminada.SetActive(false);
                }
            }
        }

        // Verificar si todos los zombies de la primera oleada han sido derrotados
        if (ataqueZombie && zombiesmision1.activeSelf && TodosZombiesMuertos(zombiesmision1))
        {
            zombiesmision1.SetActive(false);
            ataqueZombie = false;
            luz.SetActive(false);
        }

        // Verificar si todos los zombies de la segunda oleada han sido derrotados
        if (ataqueZombie && zombiesmision2.activeSelf && TodosZombiesMuertos(zombiesmision2))
        {
            zombiesmision2.SetActive(false);
            ataqueZombie = false;
            luz.SetActive(false);
        }

        //Debug.Log("ataque zombie"+ataqueZombie);
        //Debug.Log("primer generador"+primergeneradorReparado);
        //Debug.Log("endialogo"+enDialogo);
        //Debug.Log("ejuagor cerca"+jugadorCerca);
        //Debug.Log("oleada completa"+oleadaCompletada);
    }
    
    //Movimiento del Personaje
    void PausarTPC()
    {
        scriptMovimiento.movementEnabled = false;
    }

    void VolverTPC()
    {
        scriptMovimiento.movementEnabled = true;
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
