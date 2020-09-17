using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPuncture : MonoBehaviour {

    [SerializeField]
    private GameObject knifePF;

    private GameObject Player;
    private Vector3 spawn;
    int amount;
    private float randomFire;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("StartAttack", 0.0f, 0.5f);
    }
    void Update()
    {
        
            randomFire = Random.Range(1,2);
        
        
        if (Player.GetComponent<PlayerController>().IsRestarting())
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
        if(this.gameObject.activeSelf == false)
        {
            Destroy(this);
        }

    }
    void LaunchKnife()
    {
        if (Player.activeSelf)
        {
            spawn = new Vector3(Random.Range(Player.transform.position.x + 10, Player.transform.position.x - 10), transform.position.y, transform.position.z);
            Instantiate(knifePF, spawn, transform.rotation * Quaternion.Euler(0, 0, -280f));
        }
        
        
    }
    void StartAttack()
    {

        for (amount = 0; amount < randomFire; amount++)
        {
            LaunchKnife();
        }
    }
}
