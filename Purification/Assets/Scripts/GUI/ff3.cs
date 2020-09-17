using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ff3 : MonoBehaviour {

    public GameObject wincan;

    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag == "Player")
        {
            wincan.SetActive(true);
        }
    }
}
