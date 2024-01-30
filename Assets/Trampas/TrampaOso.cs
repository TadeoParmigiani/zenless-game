using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampaOso : MonoBehaviour
{
    public int dañoTrampa; // Daño de la trampa (consultar porque se hace el doble de daño)
    private float stunFactor = 0f; // Graduacion del stun (si quiere stunearlo = 0, realentizarlo > 0)
    public List<GameObject> tiposZombiesAfectables = new List<GameObject>(); // Lista de zombies a los que afecta

    // Funcionamiento de la trampa
    public void OnTriggerEnter(Collider other)
    {
        // Compara tags de zombies
        if (other.gameObject.CompareTag("zombie"))
        {
            // Afecta a la lista de zombies
            foreach (GameObject tipoZombie in tiposZombiesAfectables)
            {
                // Toma el script ControladorZombie
                ControladorZombie zombie = other.gameObject.GetComponent<ControladorZombie>(); 
                if (zombie != null)
                {
                    // Aplica Daño a la vida y lo deja quieto
                    zombie.TakeDamage(dañoTrampa);
                    zombie.ApplyStunEffect(stunFactor);
                }
            }
        }
    }
}
