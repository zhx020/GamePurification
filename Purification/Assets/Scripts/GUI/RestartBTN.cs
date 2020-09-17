using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartBTN : MonoBehaviour {

    public GameObject targetPanel;

    public void restart()
    {
        PlayerPrefs.SetInt("currentscore", 0);
        PlayerPrefs.SetInt("currentHealth", 6);
        PlayerPrefs.SetInt("currentBombs", 0);

        Time.timeScale = 1;
        if (SceneManager.GetActiveScene().name != null)
        {
            SceneManager.LoadScene(sceneName: SceneManager.GetActiveScene().name);
        }
        targetPanel.SetActive(false);
    }
}
