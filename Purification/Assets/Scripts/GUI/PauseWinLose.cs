using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseWinLose : MonoBehaviour {

    public GameObject pausePanel;
    public GameObject losePanel;
    public GameObject winPanel;

    // Use this for initialization
    void Start () {
        pausePanel.SetActive(false);
        losePanel.SetActive(false);
        winPanel.SetActive(false);
        Time.timeScale = 1f;
    }
	
    public void PauseGame(){
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ExitPause(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // this function is the same as the restart function, need to check later in the runtime
    } 

    public void BackToMenu(){

    }

    public void RestartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
