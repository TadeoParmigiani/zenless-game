using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosiva : MonoBehaviour
{
    public zombieHealth dañoaZombie;
    public int dañoTrampa = 50; // Daño que la trampa inflige a los zombies

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "zombie")
        {
            if (dañoaZombie != null)
            {
                dañoaZombie.TakeDamage(dañoTrampa);

            }
        }
    }
}
