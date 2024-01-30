using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TiempoRespawn : MonoBehaviour
{
    public float tiempoInicial = 10f; // Tiempo inicial en segundos
    private float tiempoRestante;
    public Text textoCuentaRegresiva; // Referencia al texto en pantalla
    public GameObject boton;
    public GameObject texto;

    private void Start()
    {
        tiempoRestante = tiempoInicial;
    }

    public IEnumerator IniciarCuentaRegresiva()
    {
        while (tiempoRestante > 0)
        {
            MostrarTiempo();
            yield return new WaitForSeconds(1);
            tiempoRestante--;
        }

        
    }

    private void MostrarTiempo()
    {
        textoCuentaRegresiva.text = "Tiempo restante: " + tiempoRestante.ToString();
    }

    private void ActivarBoton()
    {
        texto.SetActive(false);
        boton.SetActive(true);
    }
}

