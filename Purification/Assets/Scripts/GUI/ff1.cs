using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ff1 : MonoBehaviour {

    // Use this for initialization
    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("Session2", LoadSceneMode.Single);
        }
    }
}
