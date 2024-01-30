using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baba : MonoBehaviour
{
    // GameObject baba - propiedad de la baba que dispara el zombie // 
    GameObject player;
    valorvidaP playerHealth;

    public float velocidad = 10f;
    public int daño = 10;

    void Start()
    {

    player = GameObject.FindGameObjectWithTag("Player");
    playerHealth = player.GetComponent<valorvidaP>();
    }
    void Update(){
    //Destroy(gameObject, 4f);
    }

    void OnCollisionEnter(Collision other)
    {
        // Verificar si el proyectil colisiona con el jugador.
        if (other.gameObject == player)
        {
            if (playerHealth != null){
            // Aplicar daño al jugador.
            playerHealth.TakeDamage(daño);
        }

        // Destruir el proyectil al impactar.
         Destroy(gameObject);
        
        }
        }
    
}
