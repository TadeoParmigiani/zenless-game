using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAgarradorMovimiento : MonoBehaviour
{
    // GameObject zombie agarrador - Movimiento del zombie agarrador 
    float distancePlayer;
    public float distance;
    GameObject player; //Variable del tipo GameObject.
    GameObject puerta;
    NavMeshAgent agent; //Variable del tipo NavMeshAgent.
    Animator anim; //Variable del tipo Animator.
    public Transform zombieAgarradorUbicacion;

    private bool agarrado;

    

    void Start()
    {
        //puerta = GameObject.FindGameObjectWithTag("Puerta");
        player = GameObject.FindGameObjectWithTag("Player"); //Busca en toda la scena al objeto con el tag player y lo guarda.
        agent = GetComponent<NavMeshAgent>(); //Se busca el componente NavMeshAgent del gameobject adjunto.
        anim = GetComponent<Animator>(); //Se busca el componente Amimator del gameobject adjunto.
        


    }

    void Update()
    {
        agarrado = GetComponent<zombieagarrador>().agarrado;

        if (player != null){
           
            distancePlayer =  Vector3.Distance(transform.position, player.transform.position);
           
            if (agarrado == true){
                    agent.SetDestination(zombieAgarradorUbicacion.transform.position);
                    animating();
            }  
            else if ( distance >= distancePlayer)
            {

                agent.SetDestination(player.transform.position); // si player no es nulo configura el destino del agente NavMeshAgent para que se dirija a la posición actual del player.
                animating(); //Llamo a la funcion
            
            }
        }
    }
    void animating(){ //Funcion por la cual mide la magnitud de la velocidad y verifica si se esta moviendo.
        if(agent.velocity.magnitude != 0){
            anim.SetBool("IsMoving", true);
            }
        else{     
            anim.SetBool("IsMoving", false);
            }

            }
}
