using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanioBala : MonoBehaviour
{
    public GameObject[] zombiePrefab;
    public int danioBala;

    void OnCollisionEnter(Collision other)
    { 
        if (other.gameObject.CompareTag("zombie"))
        {
            foreach (GameObject tipozombie in zombiePrefab)
            {
                ControladorZombie zombie = other.gameObject.GetComponent<ControladorZombie>();
                if (zombie != null)
                {
                    zombie.TakeDamage(danioBala);
                }
            }

            Destroy(gameObject); // Esto se ejecuta fuera del bucle foreach para destruir el objeto una vez que se ha aplicado el daño a todos los zombies
        }
    }
}



