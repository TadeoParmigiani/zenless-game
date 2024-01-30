using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBaba : MonoBehaviour
{
    float distancePlayer;
    public float distance;
    public GameObject proyectilPrefab;
    public Transform puntoDisparo;
    public Transform player;
    public float frecuenciaDisparo = 5f;

    private float tiempoUltimoDisparo = 0f;

    void Start() 
    {
        player = GameObject.FindWithTag("Player").transform;
    }
    void Update()
    {
        distancePlayer =  Vector3.Distance(transform.position, player.transform.position);
        if ( distance >= distancePlayer)
        {
        // Verificar si es el momento de disparar.
            if (Time.time - tiempoUltimoDisparo > frecuenciaDisparo)
            {
             Disparar();
                tiempoUltimoDisparo = Time.time;
             }
        }
    }

    void Disparar()
    {
        // Calcula la dirección desde el zombie hacia el jugador.
        Vector3 direccionAlJugador = (player.position - puntoDisparo.position).normalized;
        // Instanciar el proyectil en el punto de disparo.
        GameObject proyectil = Instantiate(proyectilPrefab, puntoDisparo.position, puntoDisparo.rotation);
        
        Rigidbody proyectilRigidbody = proyectil.GetComponent<Rigidbody>();
        // Aplicar una fuerza hacia arriba para contrarrestar la gravedad.
         proyectilRigidbody.AddForce(Vector3.up * 5f, ForceMode.Impulse);  
         //Aplica una fuerza al proyectil en la dirección hacia el jugador.
        proyectilRigidbody.AddForce(direccionAlJugador * 20f , ForceMode.VelocityChange);
        Destroy(proyectil, 4f);
        
    }
}