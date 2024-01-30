using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaControl : MonoBehaviour
{

    Rigidbody bulletRb; //bala 


// poder de la bala y su tiempo de vida
    public float poderBala = 0f; 
    public float lifeTime = 4f;

    private float time = 0f;


    // Start is called before the first frame update
    void Start()
    {
        bulletRb = GetComponent<Rigidbody>(); // llama a la bala 

        bulletRb.velocity = this.transform.forward * poderBala; //velocidad de la bala


    }

    // si el tiempo en que la bala sigue con vida es mayor o igual al tiempo de vida de la bala, se destruye
    void FixedUpdate()
    {
        time += Time.deltaTime;

        if (time >= lifeTime)
        {
            Destroy(this.gameObject);
        }
    }
}