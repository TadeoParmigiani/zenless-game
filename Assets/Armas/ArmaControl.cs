using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmaControl : MonoBehaviour
{
    // Referencias a objetos y variables públicas
    public Transform shootSpawn; 
    public GameObject bulletPrefab;
    public int balas;
    public int balasTot;
    public Text balasTXT;
    public Text balasTotTXT;

    private void Update()
    {
        Debug.DrawLine(shootSpawn.position, shootSpawn.position + shootSpawn.forward * 10f, Color.red); // línea para ver dónde sale la bala
        Debug.DrawLine(Camera.main.transform.position, Camera.main.transform.position + Camera.main.transform.forward * 10f, Color.blue); // línea para ver dónde apunta la cámara

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(balas > 0)
            {
                Shoot();
                balas -= 1;
            }
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            balas += balasTot;
            balasTot = 0;
        }

        balasTXT.text = balas.ToString();
        balasTotTXT.text = balasTot.ToString();
    }

    // Método para disparar
    public void Shoot()
    {
        Instantiate(bulletPrefab, shootSpawn.position, shootSpawn.rotation);
    }
}


