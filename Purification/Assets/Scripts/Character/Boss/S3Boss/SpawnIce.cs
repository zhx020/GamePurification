using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIce : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    private GameObject knifePF;
    
    private GameObject Player;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("LaunchKnife",0f,2f);
    }
    void Update()
    {
        
    }
    void LaunchKnife()
    {
        if (Player.activeSelf)
        {
            Instantiate(knifePF, transform.position, transform.rotation);
        }

    }
}
