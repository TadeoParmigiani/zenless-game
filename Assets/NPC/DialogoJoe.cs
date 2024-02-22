using UnityEngine;

public class DialogoJoe : MonoBehaviour
{
    public GameObject[] secuenciaDialogos1;
    public GameObject[] secuenciaDialogos2;
    public GameObject[] oleadaZombie1;
    public GameObject[] oleadaZombie2;
    public GameObject mensajeInteractuar;
    private int indiceDialogoActual = 0;
    private bool enDialogo = false;
    private bool jugadorCerca = false;
    private bool primerDialogoCompletado = false;
    private bool segundoDialogoCompletado = false;
    private bool ataqueZombie = false;

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

    void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(KeyCode.F) && !ataqueZombie)
        {
            if (!primerDialogoCompletado && !segundoDialogoCompletado)
            {
                IniciarSecuenciaDialogos(secuenciaDialogos1);
            }
            else if (primerDialogoCompletado && !segundoDialogoCompletado)
            {
                IniciarSecuenciaDialogos(secuenciaDialogos2);
            }
        }

        if (enDialogo && Input.GetKeyDown(KeyCode.F))
        {
            OcultarDialogoActual(ObtenerSecuenciaActual());

            if (indiceDialogoActual < ObtenerSecuenciaActual().Length - 1)
            {
                indiceDialogoActual++;
                MostrarDialogoActual(ObtenerSecuenciaActual());
            }
            else
            {
                enDialogo = false;
                Volver();

                if (!primerDialogoCompletado)
                {
                    primerDialogoCompletado = true;
                    ataqueZombie = true;
                    IniciarAtaqueZombie(oleadaZombie1);
                }
                else if (primerDialogoCompletado && !segundoDialogoCompletado)
                {
                    segundoDialogoCompletado = true;
                    ataqueZombie = true;
                    IniciarAtaqueZombie(oleadaZombie2);
                }
            }
        }

        //VerificarExistenciaZombies1();
        //VerificarExistenciaZombies2();
    }

    void IniciarSecuenciaDialogos(GameObject[] secuencia)
    {
        indiceDialogoActual = 0;
        OcultarMensajeInteractivo();
        enDialogo = true;
        Pausar();
        MostrarDialogoActual(secuencia);
    }

    void IniciarAtaqueZombie(GameObject[] oleadaZombie)
    {
        // Implementa la lógica para activar y manejar el ataque de los zombies
        // Puedes utilizar alguna lógica similar a la de las secuencias de diálogo
    }

    void MostrarMensajeInteractivo()
    {
        mensajeInteractuar.SetActive(true);
    }

    void OcultarMensajeInteractivo()
    {
        mensajeInteractuar.SetActive(false);
    }

    void MostrarDialogoActual(GameObject[] secuencia)
    {
        secuencia[indiceDialogoActual].SetActive(true);
    }

    void OcultarDialogoActual(GameObject[] secuencia)
    {
        secuencia[indiceDialogoActual].SetActive(false);
    }

    void Volver()
    {
        Time.timeScale = 1f;
    }

    void Pausar()
    {
        Time.timeScale = 0f;
    }

    void VerificarExistenciaZombies1()
    {
        if (oleadaZombie1.Length == 0 || TodosZombiesMuertos(oleadaZombie1))
        {
            ataqueZombie = false;
        }
    }

    void VerificarExistenciaZombies2()
    {
        if (oleadaZombie2.Length == 0 || TodosZombiesMuertos(oleadaZombie2))
        {
            ataqueZombie = false;
        }
    }

    bool TodosZombiesMuertos(GameObject[] zombies)
    {
        foreach (GameObject zombie in zombies)
        {
            if (zombie != null)
            {
                return false; // Aún hay al menos un zombie vivo
            }
        }
        return true; // Todos los zombies están muertos
    }


    GameObject[] ObtenerSecuenciaActual()
    {
        if (!primerDialogoCompletado)
            return secuenciaDialogos1;
        else if (primerDialogoCompletado && !segundoDialogoCompletado)
            return secuenciaDialogos2;
        // Añade más casos según sea necesario
        return null;
    }
}