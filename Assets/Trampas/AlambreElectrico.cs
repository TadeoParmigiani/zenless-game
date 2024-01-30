using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlambreElectrico : MonoBehaviour
{
    public float stunFactor = 0.0f; // Factor de aturdimiento
    public float totalDuration = 10.0f; // Duración total del aturdimiento
    public float initialStunDuration = 1.0f; // Duración inicial del aturdimiento
    public float intervalStunDuration = 1.0f; // Duración del aturdimiento en cada intervalo
    public float stunInterval = 2.0f; // Intervalo de tiempo entre los efectos de aturdimiento
    public List<GameObject> tiposZombiesAfectables = new List<GameObject>(); // Lista de zombies a los que afecta

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("zombie"))
        {
            StartCoroutine(ApplyEffects(other.gameObject));
        }
    }

    IEnumerator ApplyEffects(GameObject zombie)
    {
        ControladorZombie controladorZombie = zombie.GetComponent<ControladorZombie>();

        // Detener al zombie por 1 segundo al tocar la trampa
        controladorZombie.ApplyStunEffect(0); // Detener el movimiento

        yield return new WaitForSeconds(initialStunDuration);

        float timer = 0.0f;

        while (timer < totalDuration)
        {
            // Aplicar el aturdimiento cada 2 segundos
            yield return new WaitForSeconds(stunInterval);
            controladorZombie.ApplyStunEffect(stunFactor);

            yield return StartCoroutine(controladorZombie.RestoreSpeedAfterDelay(intervalStunDuration)); // Restaurar velocidad después del aturdimiento

            timer += stunInterval + intervalStunDuration;
        }
    }
}
