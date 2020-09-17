using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour {

    [SerializeField] private float JUMP_HEIGHT = 2000f;                          // Amount of force added when the player jumps.
    [Range(0, .3f)] [SerializeField] private readonly float movementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] private bool airControl;                         // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask whatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform ground_check;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform ceiling_check;                          // A position marking where to check for ceilings

    public GameObject hurtBlood;
    public GameObject ingameMenu;
    public GameObject LosePanel;

    private bool grounded;            // Whether or not the player is grounded.
    const float groundedRadius = 1f;
    private Rigidbody2D player;
    private Animator animator;
    public bool facingRight = true;  // For determining which way the player is currently facing.
    private Vector2 playerVelocity = Vector3.zero;


    private Hearthealth health;
    private Color originColor;
    public bool restarted;
    public static PlayerController Instance;
    public float skillSpeed;
    public float skillCD;
    float currCD;
    bool canSkill = true;
    bool isKicking = false;

    // audio source
    public AudioClip jumpaudioclip;
    public AudioClip shootaudioclip;
    public AudioClip hurtaudioclip;
    public AudioClip kickaudioclip;


    private void Awake()
    {
        Instance = this;
        player = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        health = GetComponent<Hearthealth>();
        originColor = GetComponent<Renderer>().material.color;
        restarted = true;
    }

    private void FixedUpdate()
    {
        //bool wasGrounded = grounded;
        grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(ground_check.position, groundedRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            grounded |= colliders[i].gameObject != gameObject;
        }
        LostGame();
        
    }

    public void Move(float move, bool jump, bool shoot, bool kick)
    {
       
        //only control the player if grounded or airControl is turned on
        if (grounded || airControl)
        {

            // Move the character by finding the target velocity
            Vector2 targetVelocity = new Vector2(move * 10f, player.velocity.y);
            // And then smoothing it out and applying it to the character
            player.velocity = Vector2.SmoothDamp(player.velocity, targetVelocity, ref playerVelocity, movementSmoothing);

           if (move > 0 && !facingRight)
            {
                Flip();
            }
            else if (move < 0 && facingRight)
            {
              Flip();
            }
            animator.SetFloat("speed", Mathf.Abs(move));
        }

        // If the player should jump...
        if (grounded && jump)
        {
            // Add a vertical force to the player.
            grounded = false;
            player.AddForce(new Vector2(0f, JUMP_HEIGHT));
            animator.SetTrigger("isJump");
            AudioSource.PlayClipAtPoint(jumpaudioclip, this.transform.position);
        }
        if (grounded && !jump)
        {
            animator.SetTrigger("stopJump");
        }
        // If the player should shoot...
        if (shoot)
        {
            animator.SetTrigger("isShoot");
            AudioSource.PlayClipAtPoint(shootaudioclip, this.transform.position);

        }
        if (!shoot)
        {
            animator.SetTrigger("stopShoot");
        }
        if (!grounded && shoot)
        {
            animator.SetTrigger("isJumpShoot");
        }
        if (kick)
        {
           
            kickSkill();
            animator.SetTrigger("isKicking");
        }
        if (!kick)
        {
            animator.SetTrigger("stopKicking");
        }
        if (!grounded && kick)
        {
            kickSkill();
            animator.SetTrigger("isJumpKicking");
        }
        if (move != 0 && kick)
        {
            kickSkill();
            animator.SetTrigger("isRunKicking");
        }

        if (move != 0 && shoot)
        {
            animator.SetTrigger("isRunShoot");

        }
    }

    void kickSkill()
    {
        if (canSkill)
        {
            isKicking = true;
            if (facingRight)
            {
                Vector2 targetVelocityx1 = new Vector2(skillSpeed, player.velocity.y);
                player.velocity = Vector2.SmoothDamp(player.velocity, targetVelocityx1, ref playerVelocity, movementSmoothing);
            }
            if (!facingRight)
            {
                Vector2 targetVelocityx2 = new Vector2(skillSpeed * -1.0f, player.velocity.y);
                player.velocity = Vector2.SmoothDamp(player.velocity, targetVelocityx2, ref playerVelocity, movementSmoothing);

            }
            AudioSource.PlayClipAtPoint(kickaudioclip, this.transform.position);
            Invoke("finishKick", 0.5f);
        }
        canSkill = false;
        currCD = Time.time;
    }

    void finishKick()
    {
        isKicking = false;
    }

    public void checkSkill()
    {
        if (Time.time - currCD >= skillCD)
        {
            canSkill = true;
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void LostGame()
    {
        if (Hearthealth.Instance.health == 0 || Input.GetKeyDown(KeyCode.R))
        {
            animator.SetTrigger("isDead");
            GetComponent<PlayerController>().enabled = false;
            GetComponent<PlayerMovement>().enabled = false;
            GetComponentInChildren<arrowControl>().enabled = false;
            GetComponentInChildren<meleecontrol>().enabled = false;
            GetComponentInChildren<kickcontrol>().enabled = false;
            Invoke("Finish", 1.5f);

        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
    
        if (col.gameObject.tag == "Enemy" || col.gameObject.tag =="EnemySkill" || col.gameObject.tag == "Boss" || col.gameObject.tag == "BossSkill")
        {
            
            bool enemyAlive = true;
            if (col.gameObject.tag == "Enemy"){
                enemyAlive = col.gameObject.GetComponent<EnemyHealth>().IsAlive();
                if (col.gameObject.GetComponent<EnemyAI>().HitHead()){
                    return;
                }
            }

            if (health.IsAlive() && restarted && enemyAlive && !isKicking)
            {
                //Vector3 hurt = (transform.position - col.transform.position)*3f + Vector3.up *1.1f;
                //hurt.y = 0;
                //player.AddForce(hurt * 20 * Time.deltaTime, ForceMode2D.Impulse);
                hurtBlood.SetActive(true);
                CameraShake.Instance.IfShake = true;
                animator.Play("Die");
                AudioSource.PlayClipAtPoint(hurtaudioclip, this.transform.position);
                GetComponent<Renderer>().material.color = Color.gray;
               
                Invoke("Hide", 1.5f);
                health.LossHP();

                GetComponentInChildren<arrowControl>().enabled = false;
                GetComponentInChildren<meleecontrol>().enabled = false;
                GetComponentInChildren<kickcontrol>().enabled = false;

                GetComponent<PlayerMovement>().enabled = false;
                restarted = false;
            }
        }
       
    }

    public bool IsRestarting(){
        return restarted;
    }

    public void Hide()
    {
        hurtBlood.SetActive(false);
        gameObject.SetActive(false);
        Invoke("Restart", 0.5f);
    }

    public void Restart()
    {
        // restart the player, enable movement
        gameObject.SetActive(true);
        GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0.5f);
        GetComponent<PlayerMovement>().enabled = true;
        Invoke("ResetRestarte", 2);
    }

    private void ResetRestarte(){

        GetComponent<Renderer>().material.color = originColor;
        GetComponentInChildren<arrowControl>().enabled = true;
        GetComponentInChildren<meleecontrol>().enabled = true;
        GetComponentInChildren<kickcontrol>().enabled = true;
        restarted = true;
    }

    void Finish(){
        LosePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public bool IsOnGround(){
        return grounded;
    }

}
