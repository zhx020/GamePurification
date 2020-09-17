using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3 : MonoBehaviour {

    public Animator anim;
    private GameObject player;
    private enemystate Cr_state = enemystate.Approach;
    public GameObject ffg;


    //patrol
    public float MoveSpeed;

    //approach

    private float DistanceToPlayer;


    //FIGHT


    public GameObject Box;
    //Skill
    private float Timer;



    

    private enum enemystate
    {

        Approach,
        VeryVeryAngry,
        Die,

    }
    // Use this for initialization
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }
    private void Start()
    {


    }
    // Update is called once per frame
    void FixedUpdate()
    {

        DistanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        switch (Cr_state)
        {

            case enemystate.Approach:
                anim.Play("Attack");
                FacePlayer();
                SkillFireball();
                CheckHealth();
                break;

            case enemystate.VeryVeryAngry:

                GoDie();

                SkillFinal();
                break;
            case enemystate.Die:

                anim.Play("Death");
                Destroy(this.gameObject, 0.5f);
                Box.SetActive(true);
                ffg.SetActive(true);
                break;


        }
    }

    void CheckHealth()
    {
        if (BossHP.Instance.Q2 == true)
        {
            Cr_state = enemystate.VeryVeryAngry;
           
        }
    }
    void GoDie()
    {
        if (BossHP.Instance.Hp <= 0)
        {
            Cr_state = enemystate.Die;
        }
    }

    void FacePlayer()
    {
        if (DistanceToPlayer >= 5)
        {
            Vector3 heroface = player.transform.position - transform.position;
            Vector3 herodirection = heroface.normalized;


            transform.rotation = Quaternion.LookRotation(herodirection);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 90, 0);
        }

    }
    void SkillFireball()
    {
        transform.GetChild(0).gameObject.SetActive(true);

    }

    void SkillFinal()
    {

        transform.GetChild(1).gameObject.SetActive(true);
        
    }
   

}
