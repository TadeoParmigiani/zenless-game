using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaNpc : MonoBehaviour
{
    public GameObject simboloTienda;
    public bool jugadorCerca;
    public bool interfazAbierta;
    public GameObject panel1;
    public GameObject panelCompra;
    public ThirdPersonControllerMovement cameraEnable;

    // Start se llama antes del primer frame
    void Start()
    {
        simboloTienda.SetActive(true);
        panelCompra.SetActive(false);
        jugadorCerca = false;
    }

    //Update se llama una vez por frame
   void Update()
{
    if (Input.GetKeyDown(KeyCode.F) && jugadorCerca == true)
    {
        if (!panelCompra.activeSelf)
        {
            panel1.SetActive(false);
            panelCompra.SetActive(true);
            Time.timeScale = 0f;
            cameraEnable.DisableMovement();
        }
        else
        {
            Volver();
        }
    }
}

    public void Volver()
    {
        Time.timeScale = 1f;
        panelCompra.SetActive(false);
        panel1.SetActive(true);
        cameraEnable.EnableMovement();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true;
            if (!interfazAbierta)
            {
                panel1.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;
            panel1.SetActive(false);
        }
    }
}

