using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : MonoBehaviour
{

    private Vector3 direction;
    
    private float Timer;
    // Use this for initialization
    void Start()
    {

       
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer > 2)
        {
            Destroy(this.gameObject);
        }
    }

    //void OnCollisionEnter2D(Collision2D col)
    //{
    //    if (col.gameObject.tag != "Enemy_Skill")
    //    {
    //        Destroy(this.gameObject);
    //    }


    //}
}
