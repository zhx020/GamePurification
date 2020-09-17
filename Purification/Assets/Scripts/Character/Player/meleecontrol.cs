using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleecontrol : MonoBehaviour {

    public Rigidbody2D melee;
    public float speed = 30f;
    float currCD;
    public float cooldownShoot = 0.3f;
    bool canShoot = true;

    private PlayerController ctrl;
    //public PlayerMovement move;
    //private Animation anim;

    void Start()
    {

    }

    void Awake()
    {
        //anim = transform.root.gameObject.GetComponent<Animation>();
        ctrl = transform.root.GetComponent<PlayerController>();
        //move = transform.root.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire2") && canShoot)
        {
            Rigidbody2D bulletInstance = Instantiate(melee, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
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
    }

    void checkCD()
    {
        if (Time.time - currCD >= cooldownShoot)
        {
            canShoot = true;
        }
    }
}
