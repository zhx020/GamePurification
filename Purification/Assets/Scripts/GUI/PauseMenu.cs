using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    public GameObject okbtn;
    public GameObject nobtn;
    public GameObject restartbtn;
    public GameObject ingameMenu;

    // Use this for initialization
    void Start()
    {
        okbtn.GetComponent<Button>().onClick.AddListener(ok);
        nobtn.GetComponent<Button>().onClick.AddListener(no);
        restartbtn.GetComponent<Button>().onClick.AddListener(restart);

    }

    // Update is called once per frame
    void Update()
    {

    }
    void ok()
    {
        Time.timeScale = 1;
        ingameMenu.SetActive(false);
    }
    void restart()
    {
        PlayerPrefs.SetInt("currentscore", 0);
        PlayerPrefs.SetInt("currentHealth", 6);
        PlayerPrefs.SetInt("currentBombs", 0);

        Time.timeScale = 1;
        if (SceneManager.GetActiveScene().name != null)
        {
            SceneManager.LoadScene(sceneName: SceneManager.GetActiveScene().name);
        }
        ingameMenu.SetActive(false);

        //Application.LoadLevel(Application.loadedLevelName);
    }
    void no()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("LevelSelector", LoadSceneMode.Single);
    }
}
