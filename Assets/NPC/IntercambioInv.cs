using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class IntercambioInv : MonoBehaviour
{
    private bool jugadorCerca = false;
    private bool mensajeMostrado = false;
    public GameObject interfazIntercambio;
    public GameObject mensajeInteractuar;
    public GameObject mensajeSinCorazones;
    public Button botonIntercambiar;
    public Button botonCerrar;
    public Text dineroTexto;
    public valorvidaP scriptvalorvidaP;
    public ThirdPersonControllerMovement cameraEnable;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && jugadorCerca && scriptvalorvidaP.corazones > 0)
        {
            Pausar();
            OcultarMensajeInteractivo();
            InteractuarConNPC();
        }
        else if (Input.GetKeyDown(KeyCode.F) && jugadorCerca && scriptvalorvidaP.corazones == 0 && !mensajeMostrado)
        {
            Pausar();
            OcultarMensajeInteractivo();
            mensajeSinCorazones.SetActive(true);
            mensajeMostrado = true;
        }
        else if (Input.GetKeyDown(KeyCode.F) && mensajeMostrado)
        {
            mensajeSinCorazones.SetActive(false);
            mensajeMostrado = false;
            Volver();
        }
    }

    public void InteractuarConNPC()
    {
        interfazIntercambio.SetActive(true);
        dineroTexto.text = "Si deseas intercambiarme los corazones recibiras: " + scriptvalorvidaP.corazones * 15 + "$"; // Precio por corazón
        botonIntercambiar.onClick.AddListener(IntercambiarCorazonesPorDinero);
        botonCerrar.onClick.AddListener(CerrarInterfaz);
    }

    public void IntercambiarCorazonesPorDinero()
    {
        int dinero = scriptvalorvidaP.corazones * 15; // Calcular el dinero a recibir
        scriptvalorvidaP.dinero += dinero; // Sumar el dinero obtenido al valor de dinero en valorvidaP
        scriptvalorvidaP.corazones = 0; // Poner la cantidad de corazones del jugador a 0
        interfazIntercambio.SetActive(false);
        Volver();
    }

    void CerrarInterfaz()
    {
        interfazIntercambio.SetActive(false);
        Volver();
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

    void MostrarMensajeInteractivo()
    {
        mensajeInteractuar.SetActive(true);
    }

    void OcultarMensajeInteractivo()
    {
        mensajeInteractuar.SetActive(false);
    }

    void Volver()
    {
        Time.timeScale = 1f;
        cameraEnable.EnableMovement();
        MostrarMensajeInteractivo();
    }

    void Pausar()
    {
        Time.timeScale = 0f;
        cameraEnable.DisableMovement();
        
    }
}