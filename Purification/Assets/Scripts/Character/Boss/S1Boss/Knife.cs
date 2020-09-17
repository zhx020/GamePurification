using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour {

    [SerializeField]
    private GameObject knifePF;

    private GameObject Player;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("LaunchKnife", 0.0f, 0.5f);
    }
	void Update () {
        
	}
    void LaunchKnife()
    {
        if (Player.activeSelf)
        {
            Instantiate(knifePF, transform.position, transform.rotation);
        }
        
    }
}
