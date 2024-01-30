using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject panelGameOver;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void GameOver(){
        panelGameOver.SetActive(true);
    }
    public void LoadScene(string name){
        UnityEngine.SceneManagement.SceneManager.LoadScene(name);
    }
}
