using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class MenuScripts : MonoBehaviour {

    public GameObject menuPanel;
    public GameObject aboutPanel;
    public GameObject introPanel;

    public void Start()
    {
        menuPanel.SetActive(false);
        aboutPanel.SetActive(false);
        introPanel.SetActive(false);
    }

    public void PressButton(){
        string buttonName= EventSystem.current.currentSelectedGameObject.name;

        switch(buttonName)
        {
            case "BtnIntro":
                introPanel.SetActive(true);
                break;
            case "BtnPlay":
                menuPanel.SetActive(true);
                break;
            case "BtnAbout":
                aboutPanel.SetActive(true);
                break;
            case "BtnAchieve":
               //SceneManager.LoadScene("HardLevel");
                break;
            case "BtnExit":
                Debug.Log("Hase quie game");
                Application.Quit();
                break;
        }
    }

    public void SelectLevel()
    {
        string buttonName = EventSystem.current.currentSelectedGameObject.name;
        switch (buttonName)
        {
            case "BtnEasy":
                PlayerPrefs.SetInt("currentscore", 0);
                PlayerPrefs.SetInt("currentHealth", 6);
                PlayerPrefs.SetInt("currentBombs", 0);
                PlayerPrefs.SetInt("Startinghealth", 60);
                SceneManager.LoadScene("Session1");
                break;
            case "BtnMedium":
                PlayerPrefs.SetInt("currentscore", 0);
                PlayerPrefs.SetInt("currentHealth", 6);
                PlayerPrefs.SetInt("currentBombs", 0);
                PlayerPrefs.SetInt("Startinghealth", 120);
                SceneManager.LoadScene("Session1");
                break;
            case "BtnHard":
                PlayerPrefs.SetInt("currentscore", 0);
                PlayerPrefs.SetInt("currentHealth", 6);
                PlayerPrefs.SetInt("currentBombs", 0);
                PlayerPrefs.SetInt("Startinghealth", 180);
                SceneManager.LoadScene("Session1");
                break;
        }
    }

    public void ExitPanel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
