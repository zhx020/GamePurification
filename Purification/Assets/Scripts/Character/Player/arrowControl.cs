using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowControl : MonoBehaviour {


    public Rigidbody2D arrow;
    public float speed = 30f;

    //float initialtime;
    float currCD;
    public float cooldownShoot = 2.0f;
    bool canShoot = true;

    private PlayerController ctrl;
    //public PlayerMovement move;
    //private Animation anim;

    //void Start()
    //{
    //    initialtime = Time.time;
    //}

    void Awake()
    {

        //anim = transform.root.gameObject.GetComponent<Animation>();
        ctrl = transform.root.GetComponent<PlayerController>();
        //move = transform.root.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Attack") && canShoot)
        {
            Rigidbody2D bulletInstance = Instantiate(arrow, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
            if (!ctrl.facingRight)
            {
                bulletInstance.velocity = new Vector2(-speed, 0);
            }
            else
            {
                bulletInstance.velocity = new Vector2(speed, 0);
            }
            canShoot = false;
            currCD = Time.time;
        }
        checkCD();
        //if(initialtime - Time.time > 1f)
        //{
        //    Destroy(gameObject);
        //}
    }

    void checkCD()
    {
        if(Time.time - currCD >= cooldownShoot)
        {
            canShoot = true;
        }
    }
}
