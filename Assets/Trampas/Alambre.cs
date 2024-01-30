using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alambre : MonoBehaviour
{
    public int dañoTrampa = 10; // Daño que la trampa inflige a los zombies
    public float slowFactor = 0.5f; // Factor de ralentización
    public int bleedDamage = 1; // Daño por sangrado
    public float bleedDuration = 10.0f; // Duración del sangrado
    public List<GameObject> tiposZombiesAfectables = new List<GameObject>(); // Lista de Zombies que afecta los "debuffos"

    // Funcionamiento de la trampa
    private void OnTriggerEnter(Collider other)
    {
        // Compara Tags de zombie
        if (other.gameObject.CompareTag("zombie"))
        {
            // Para cada zombie de la lista:
            foreach (GameObject tipoZombie in tiposZombiesAfectables)
            {
                // Toma el script de ControladorZombie
                ControladorZombie zombie = other.gameObject.GetComponent<ControladorZombie>();
                if (zombie != null)
                {
                    // Aplica los 
                    zombie.ApplySlowEffect(slowFactor);
                    zombie.ApplyBleedEffect(bleedDamage, bleedDuration);
                }
            }
        }
    }
}