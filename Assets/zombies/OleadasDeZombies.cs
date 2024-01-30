using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OleadasDeZombies : MonoBehaviour
{
    // GamObject Controlador de zombies - Genera las oleadas de los zombies cuando se cumple un horario  especifico //
    public GameObject[] zombiePrefab;
    public GameObject[] puntosDeSpawn;
    public int cantidadEnemigos;
    public int oleada;
    private bool noche;
    bool ocurrioLaOleada = false;
    public int tipoDeEnemigo;
    public float nocheStart;   // La hora en la que se considera que comienza la noche.
    //(19 hs * 60(segudos q dura el dia) / 24hs) el rusultado sobre 60
    public float nocheEnd;      // La hora en la que se considera que termina la noche.
    private ciclodiaynoche tiempoDia;
    public GameObject sol;
   
    // Start is called before the first frame update
    void Start()
    {
        tiempoDia = sol.GetComponent<ciclodiaynoche>();
        cantidadEnemigos = 1;
        oleada = 1;

    }

    // Update is called once per frame
    void Update()
    {
        float tiempoDelDiaa = tiempoDia.tiempoDelDia;
        noche = EsDeNoche(tiempoDelDiaa);

        // Comprobar si es de noche y si no se ha producido una oleada en este día.
        if (noche && ocurrioLaOleada == false)
        {
            StartCoroutine(ProcesoOleada());
            ocurrioLaOleada = true; // Marcar que ya se ha producido la oleada.
        }

        // Reiniciar la marca de la oleada al día siguiente.
        if (!noche)
        {
            ocurrioLaOleada = false;
        }
    }
    // Spawn de enemigos
    IEnumerator ProcesoOleada()
    {
        for (int i = 0; i < cantidadEnemigos; i++)
        {
            SpawnEnemigos(cantidadEnemigos);
            yield return new WaitForSeconds(2);
        }

        oleada += 1;
        cantidadEnemigos += 1;
    }
    
    bool EsDeNoche(float tiempo)
    {
        // Verifica si es de noche en función de la hora actual del ciclo día/noche.
        if(tiempo >= nocheStart || tiempo < nocheEnd){
            return true;
        }
        else{
            return false;
        }
    }
    // Aleatorizador de zombies
    void SpawnEnemigos(int oleada){
        int spawn = Random.Range(0,3);
        if (oleada == 1){
            tipoDeEnemigo = 1;
        }
        else if (oleada == 2){
            tipoDeEnemigo = Random.Range(0, 2);
        }
        else {
        tipoDeEnemigo = Random.Range(0, 3);
        }

        Instantiate(zombiePrefab[tipoDeEnemigo], puntosDeSpawn[spawn].transform.position, puntosDeSpawn[spawn].transform.rotation);

    }
}