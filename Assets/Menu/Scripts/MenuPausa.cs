using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    [SerializeField] private GameObject menuPausa;

    private bool juegoPausado = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (juegoPausado)
            {
                Reanudar();
            }
            else
            { 
                Pausa();               
            }
        }
    }

    public void Pausa()
    {
        juegoPausado = true;
        Time.timeScale = 0f;
        menuPausa.SetActive(true);
    }
    
    public void Reanudar()
    {
        juegoPausado = false;
        Time.timeScale = 1f;
        menuPausa.SetActive(false);
    }

    public void Controles()
    {
        juegoPausado = false;
        Time.timeScale = 0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);        
    }

    public void MenuPrincipal()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    
}
