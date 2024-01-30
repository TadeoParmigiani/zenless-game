using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class volverprincipio : MonoBehaviour
{
    // Start is called before the first frame update
  void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void EscenaMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void EscenaReintetar()
    {
        SceneManager.LoadScene("Juego");
    }

}
