using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorDialogo : MonoBehaviour
{
    public GameObject[] secuenciaDialogos;
    private int indiceDialogoActual = 0;
    private bool enDialogo = false;
    public GameObject collider;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IniciarSecuenciaDialogos();
        }
    }

    void IniciarSecuenciaDialogos()
    {
        enDialogo = true;
        Pausar();
        MostrarDialogoActual();
    }

    void MostrarDialogoActual()
    {
        secuenciaDialogos[indiceDialogoActual].SetActive(true);
    }

    void OcultarDialogoActual()
    {
        secuenciaDialogos[indiceDialogoActual].SetActive(false);
    }

    void Update()
    {
        if (enDialogo && Input.GetKeyDown(KeyCode.F))
        {
            OcultarDialogoActual();

            if (indiceDialogoActual < secuenciaDialogos.Length - 1)
            {
                indiceDialogoActual++;
                MostrarDialogoActual();
            }
            else
            {
                enDialogo = false;
                Volver();
                collider.SetActive(false);
            }
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
}