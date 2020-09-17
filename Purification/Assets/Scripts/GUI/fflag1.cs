using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fflag1 : MonoBehaviour
{


    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("BossScene", LoadSceneMode.Single);
        }
        PlayerPrefs.SetInt("bs", 1);
    }
}

