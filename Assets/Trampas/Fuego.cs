using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuego : MonoBehaviour
{
    public int dañoTrampa = 10; // Daño que la trampa inflige a los zombies
    public float slowFactor = 0.5f; // Factor de ralentización
    public int burnDamage = 5; // Daño por fuego
    public float burnDuration = 5.0f; // Duración del fuego

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("zombie"))
        {
            ControladorZombie zombie = other.gameObject.GetComponent<ControladorZombie>();
            if (zombie != null)
            {
                zombie.ApplySlowEffect(slowFactor);
                zombie.ApplyBurnEffect(burnDamage, burnDuration);
            }
        }
    }
}

