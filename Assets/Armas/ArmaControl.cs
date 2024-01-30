using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmaControl : MonoBehaviour
{
// 
    public Transform shootSpawn; 

    public bool shooting = false;

    public GameObject bulletPrefab;

    public int cargador;
    public int balasTot;
    public int balas;


    public Text balasTXT;
    public Text balasTotTXT;


    private void Update()
    {
        Debug.DrawLine(shootSpawn.position, shootSpawn.forward * 10f, Color.red); //linea para ver donde sale la bala
        Debug.DrawLine(Camera.main.transform.position, Camera.main.transform.forward * 10f, Color.blue); //linea para ver donde apunta la camara


        RaycastHit cameraHit; // donde golpea la linea de la camara, corrige el shootSpawn, asi la bala golpea adonde mira la camara

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out cameraHit))
        {
            Vector3 shootDirection = cameraHit.point - shootSpawn.position;
            shootSpawn.rotation = Quaternion.LookRotation(shootDirection);

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
                if(balasTot != 0)
                {
                    if (balas != cargador)
                    {
                        if (balasTot > balas)
                        {
                            balasTot = balasTot - cargador + balas;
                            balas = cargador;
                        }
                
                        else
                        {
                            balas = balasTot;
                            balasTot = 0;     
                        }   
                    }
                }       
            }

            balasTXT.text = balas.ToString();
            balasTotTXT.text = balasTot.ToString();
        }
    }

    public void Shoot()
    {
        Instantiate(bulletPrefab, shootSpawn.position, shootSpawn.rotation);
    }
}


