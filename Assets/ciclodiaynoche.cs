using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ciclodiaynoche : MonoBehaviour
{
    public Light solyluna; // Luz del sol o luna.
    public float diaDuracion; // Duración de un día en segundos.
    public float tiempoDelDia = 0.0f;     // Hora del día en segundos.
    private int diasContador = 1;          // Contador de días.

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         // Avanzar el tiempo en función de la duración del día.
        tiempoDelDia += Time.deltaTime / diaDuracion;

        // Si pasa un día, actualiza el contador de días.
        if (tiempoDelDia >= 1.0f)
        {
            tiempoDelDia = 0.0f;
            diasContador++;
        }

        // Actualiza la rotación de la luz (sol o luna) para simular el ciclo día-noche.
        UpdateDayNightCycle();

        // Puedes imprimir el tiempo actual y el número de días en la consola para depuración.
       Debug.Log("Día " + diasContador + ", Hora: " + Mathf.Floor(tiempoDelDia * 24) + ":00");
       // Debug.Log("Hora actual: " + GetFormattedTime());
    }
    
    private void UpdateDayNightCycle(){
        // Actualiza la rotación de la luz (sol o luna) para simular el ciclo día-noche.
        float angulo =  Mathf.Lerp(-90, 270, tiempoDelDia); // Ángulo en grados para un ciclo completo de 0 a 360 grados.
        solyluna.transform.rotation = Quaternion.Euler(new Vector3 (angulo, 0, 0));
    }
    
}
