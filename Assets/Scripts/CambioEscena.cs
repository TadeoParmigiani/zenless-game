using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // sirve para habilitar comandos de scene manager.

public class CambioEscena : MonoBehaviour
{
  
  public void CambiarEscena(string nombre)

 {
   SceneManager.LoadScene(nombre); // carga la escena al nombre que le hayamos puesto.
 }

}