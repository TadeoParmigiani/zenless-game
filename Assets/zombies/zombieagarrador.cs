using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieagarrador : MonoBehaviour
{
    // GameObject zombie agarrador - genera una fuerza hacia el zombie cuando el player entra a un cierto rango //
    public float fuerzaAtraccion;
    //private Vector3 puntoDeAgarre;
    float distancePlayer;
    public float distance;
    float distanceLengua = 5f; 
    //public GameObject LenguaPrefab;
    public Transform puntoDisparo;
    public Transform player;
    float frecuenciaDisparo = 3f;
    public bool lenguaExtendida = false;
    private float tiempoUltimoDisparo = 0f;
    public bool agarrado = false;
    
    public void ArrastraDeshabilitado() { lenguaExtendida = false; } 

    // Start is called before the first frame update
    void Start()
    {
        
        player = GameObject.FindWithTag("Player").transform;
        

    }

    // Update is called once per frame
    void Update()
    {
        distancePlayer =  Vector3.Distance(transform.position, player.transform.position);
        if ( distance >= distancePlayer && !lenguaExtendida)
        {
            ExtendLengua();     
        }
        if (lenguaExtendida)
        {
        ArrastraJugador();
            agarrado = true;
        }

    }
    public void ExtendLengua()
    {
        Vector3 direccionAlJugador = (player.position - puntoDisparo.position).normalized;
        
        RaycastHit hit;
        Debug.DrawRay(transform.position, direccionAlJugador * distanceLengua, Color.green);
        
        if(Physics.Raycast(transform.position, direccionAlJugador, out hit, distanceLengua))
        {
            if (hit.collider.CompareTag("Player"))
            {
                lenguaExtendida = true;
               
            }
        
        }
    }   
     void ArrastraJugador()
    {
        Vector3 direccionAlJugador = (player.position - puntoDisparo.position).normalized;
        //player.transform.position = Vector3.MoveTowards(player.transform.position, puntoDeAgarre, velocidadArrastre * Time.deltaTime);
         Rigidbody playerRigidbody = player.GetComponent<Rigidbody>();
            playerRigidbody.AddForce(-direccionAlJugador * fuerzaAtraccion, ForceMode.Force);
    }

}
