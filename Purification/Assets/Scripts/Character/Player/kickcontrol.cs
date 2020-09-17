using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kickcontrol : MonoBehaviour {

    public Rigidbody2D kick;
    public float speed = 30f;
    public float cooldownShoot = 2.0f;

    private PlayerController ctrl;

    void Awake()
    {

        ctrl = transform.root.GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            Rigidbody2D bulletInstance = Instantiate(kick, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
            bulletInstance.velocity = !ctrl.facingRight ? new Vector2(-speed, 0) : new Vector2(speed, 0);
        }
    }
}
