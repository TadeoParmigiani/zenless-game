using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColocarTrampa : MonoBehaviour
{
    public GameObject interfaztrampa1; // Trampa de Oso
    public GameObject interfaztrampa2; // Trampa de Alambre
    public GameObject interfaztrampa3; // Trampa Eléctrica
    public GameObject interfaztrampa4; // Trampa de Fuego
    public GameObject interfaztrampa5; // Trampa Explosiva
    public GameObject trampa1; // Trampa de Oso
    public GameObject trampa2; // Trampa de Alambre
    public GameObject trampa3; // Trampa Eléctrica
    public GameObject trampa4; // Trampa de Fuego
    public GameObject trampa5;
    public GameObject interfazColocacion;
    public bool interfazColocacionActiva;
    private GameObject trampaSeleccionada; // Muestra la trampa seleccionada
    private GameObject trampaColocada; // Coloca la trampa seleccionada

    public void Start()
    {
        DesactivarTodasLasTrampas();
        interfazColocacion.SetActive(false);
    }

    // Desactivar todas las trampas
    public void DesactivarTodasLasTrampas()
    {
        interfaztrampa1.SetActive(false);
        interfaztrampa2.SetActive(false);
        interfaztrampa3.SetActive(false);
        interfaztrampa4.SetActive(false);
        interfaztrampa5.SetActive(false);
        trampa1.SetActive(true);
        trampa2.SetActive(true);
        trampa3.SetActive(true);
        trampa4.SetActive(true);
        trampa5.SetActive(true);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (interfazColocacion.activeSelf)
            {
                Debug.Log("Modo Colocación Desactivado");
                interfazColocacion.SetActive(false);
                DesactivarTodasLasTrampas();
            }
            else
            {
                Debug.Log("Modo Colocación Activado");
                interfazColocacion.SetActive(true);
            }
        }

        if (interfazColocacion.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                DesactivarTodasLasTrampas();
                interfaztrampa1.SetActive(true);
                trampaSeleccionada = trampa1;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                DesactivarTodasLasTrampas();
                interfaztrampa2.SetActive(true);
                trampaSeleccionada = trampa2;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                DesactivarTodasLasTrampas();
                interfaztrampa3.SetActive(true);
                trampaSeleccionada = trampa3;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                DesactivarTodasLasTrampas();
                interfaztrampa4.SetActive(true);
                trampaSeleccionada = trampa4;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                DesactivarTodasLasTrampas();
                interfaztrampa5.SetActive(true);
                trampaSeleccionada = trampa5;
            }

            if (Input.GetKeyDown(KeyCode.E) && trampaSeleccionada != null)
            {
                ColocarTrampaSeleccionada();
            }
        }
    }

    public void ColocarTrampaSeleccionada()
    {
        if (trampaSeleccionada != null)
        {
            Vector3 posicionTrampa = transform.position + transform.forward * 2f; 
            Quaternion rotacionTrampa = trampaSeleccionada.transform.rotation; 
            trampaColocada = Instantiate(trampaSeleccionada, posicionTrampa, rotacionTrampa);
        }
    }

}
