using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class pauseBTN : MonoBehaviour {

    public GameObject pausePanel;
    bool isPaused = false;

    public void pauseGame(){
        if(!isPaused){
            Time.timeScale = 0;
            isPaused = true;
            pausePanel.SetActive(true);

        }
        else
        {
            Time.timeScale = 1;
            isPaused = false;
        }
    }


}
