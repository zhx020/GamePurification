using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSkill : MonoBehaviour {
    private Vector3 direction;
    private GameObject player;
    private float MoveSpeed = 15f;
    private float Timer;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");

        direction = (player.transform.position - transform.position).normalized;
        direction.y = 0;
        direction.z = 0;
    }

    // Update is called once per frame
    void Update()
    {


        AttackPlayer();
        Timer += Time.deltaTime;
        if (Timer > 5)
        {
            Destroy(this.gameObject);
        }
    }
    void AttackPlayer()
    {
        transform.position += direction.normalized * MoveSpeed * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag != "Enemy_Skill")
        {
            Destroy(this.gameObject);
        }


    }
}