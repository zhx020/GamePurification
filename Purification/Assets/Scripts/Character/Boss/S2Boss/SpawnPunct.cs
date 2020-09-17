using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPunct : MonoBehaviour {

    private float Timer;
    private float DropSpeed;
    private float RandomTimer;
    private GameObject player;
    // Use this for initialization
    void Start()
    {
        
    }
        // Update is called once per frame
        void Update () {
        DropSpeed = Random.Range(5,30);
        RandomTimer = Random.Range(1, 2);
        
        Timer += Time.deltaTime;
        if(BossHP.Instance.Q2 == true)
        {

        }
        if(Timer>RandomTimer)
        {
            transform.position +=(Vector3.down * Time.deltaTime * DropSpeed);
        }
        if(Timer>5)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}
