using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Puerta : MonoBehaviour
{ // GameObject puerta - cierra y abre la puerta//
    public GameObject puertaCerrada;
    public GameObject puertaAbierta;
    public ThirdPersonControllerMovement cameraEnable;
    
    // Start is called before the first frame update
    void Start()
    {
        puertaAbierta.SetActive(false);
        puertaCerrada.SetActive(true);
        Debug.Log(cameraEnable);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (puertaCerrada.activeSelf)
            {
                puertaAbierta.SetActive(true);
                puertaCerrada.SetActive(false);
            }
        }
        if (other.CompareTag("zombie"))
        {
            if (cameraEnable != null)
            {
                SceneManager.LoadScene("GameOver");
                cameraEnable.DisableMovement();
        
            }
        }
        
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            puertaAbierta.SetActive(false);
            puertaCerrada.SetActive(true);
        }
    }
}
