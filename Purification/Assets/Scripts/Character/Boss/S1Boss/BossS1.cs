using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossS1 : MonoBehaviour
{

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
    
    private float boomMoveSpeed = 30;

    
 
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


                Timer += Time.deltaTime;

                if (Timer < 3)
                {
                    SkillFinal();
                }
                if (Timer > 5)
                {

                    FinalL();

                }
                if (Timer > 10)
                {
                    Timer = 0;
                }
                break;
            case enemystate.Die:
                Debug.Log("die");
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
            Destroy(transform.GetChild(1).gameObject);
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
        Timer += Time.deltaTime;
        if(Timer>5)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
        }

        

    }

    void SkillFinal()
    {
        transform.localScale = new Vector3(-1.3f, 1.3f, 0);
        //direction = (player.transform.position - transform.position).normalized;
        //direction.y = 0;
        //transform.position += direction.normalized * boomMoveSpeed * Time.deltaTime;           
        transform.position += Vector3.right * boomMoveSpeed * Time.deltaTime;
       
    }
    void FinalL()
    {
        
        transform.localScale = new Vector3(1.3f, 1.3f, 0);
        transform.position += Vector3.left * boomMoveSpeed * Time.deltaTime;
    }
}


