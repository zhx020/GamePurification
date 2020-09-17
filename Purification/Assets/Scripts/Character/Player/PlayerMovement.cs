using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public PlayerController controller;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump;
    bool shoot;
    bool kick;

    // Update is called once per frame
    void Update()
    {

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        jump |= Input.GetButtonDown("Jump");

        shoot |= Input.GetButtonDown("Attack");

        kick |= Input.GetButtonDown("Fire3");

        controller.checkSkill();

    }

    void FixedUpdate()
    {
        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump, shoot, kick);
        jump = false;
        shoot = false;
        kick = false;
    }
}
