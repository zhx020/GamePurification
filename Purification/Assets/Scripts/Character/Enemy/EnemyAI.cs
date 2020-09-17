using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{


    public GameObject explosion;
    public float visibleDis = 15.0f;
    public float attackDis = 12.0f;
    public Transform leftRangePoint;
    public Transform rightRangePoint;
    public bool idle;
    public bool isVampire;
    public GameObject coin;
    public bool isEnemyArmy;
    public AudioClip explosionSound;

    private GameObject player;
    // the attack range and next going point when it is approaching to player
    private Vector3[] attackRange;
    private int closetsPos;
    private bool startApproach; // used to make a one call in finding the closest point to player before approaching

    private bool can_attack = true;
    private bool isActive = true;
    private EnemyHealth health;     //enemy health component
    private EnemyMovement movement; // enemy movement component
    private Animator anim;
    private bool hitHead;
    private CircleCollider2D circleCollider;
    private float nextBounceTime;
    private Color originColor;

    DecisionTree root;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        attackRange = new Vector3[2];
        anim = GetComponent<Animator>();
        health = GetComponent<EnemyHealth>();
        movement = GetComponent<EnemyMovement>();
        circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.isTrigger = true;
        nextBounceTime = Time.time + 2f;
        hitHead = false;
        originColor = GetComponent<Renderer>().material.color;
        startApproach = true;
    }

    // Use this for initialization
    void Start()
    {
        BuildDecisionTree();
        player = GameObject.FindGameObjectWithTag("Player");
        SetAttackRange();
    }

    private void SetAttackRange()
    {
        attackRange[0] = new Vector3(leftRangePoint.position.x, leftRangePoint.position.y + movement.GetEnemyHeight(), leftRangePoint.position.z);
        attackRange[1] = new Vector3(rightRangePoint.position.x, rightRangePoint.position.y + movement.GetEnemyHeight(), rightRangePoint.position.z);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (isActive && !explosion.activeSelf){
            root.Search();
        }
    }

    public void SetAnimator(Animator new_anim)
    {
        anim = new_anim;
        anim.applyRootMotion = false;
    }

    void EnableAttack()
    {
        can_attack = !can_attack;
    }

    void SetActive()
    {
        isActive = true;
    }

    void SetInactive()
    {
        gameObject.SetActive(false);
    }

    /* Make Decision */

    bool IsAlive()
    {
        return health.IsAlive();
    }

    bool IsVisible()
    {
        GetComponent<Renderer>().material.color = originColor;

        if (player != null){
            float playerDist = Vector3.Distance(gameObject.transform.position, player.transform.position);
            return (playerDist < visibleDis ? true : false) && player.activeSelf;
        }else{
            return false;
        }
    }

    bool InAttackRange()
    {
        //check if player is within the attack range to the closets point. if it is, attack
        float playerDis = Vector3.Distance(gameObject.transform.position, player.transform.position);  // distance to the left range point, default settings

        return (playerDis <= attackDis ? true : false) 
            && (attackRange[0].x<=player.transform.position.x && attackRange[1].x >= player.transform.position.x)
            && Mathf.Abs(player.transform.position.y-transform.position.y)<0.2f ;
    }

    /* Actions */

    void Patrol()
    {

        if (idle){
            // if the enemy leaves its origing standing position, walk back to that point
            if (!movement.IsAtOriginPos()){
                anim.Play("Walk");
                movement.Patrol(false);
            }
            else{
                anim.Play("Idle");
            }
        }
        else{
            anim.Play("Walk");
            movement.Patrol(true);
        }

        startApproach = true;   // set startApproach to true for next time finding the closest point
    }

    void Die()
    {
       // falling coins
        float coinNumber = Random.Range(0, 2);

        for (int i = 0; i < coinNumber; i++)
        {
            Vector3 position = new Vector3(transform.position.x + i * 4, transform.position.y-0.5f, transform.position.z);
            GameObject star = Instantiate(coin, position, Quaternion.Euler(new Vector3(0, 0, 0)));
            Destroy(star, 3f);
        }

        // die
        anim.Play("Die");

       
        GetComponent<Renderer>().material.color = Color.gray;
        if (isEnemyArmy)
        {
            Invoke("SetInactive", anim.GetCurrentAnimatorStateInfo(0).length);
        }
        else
        {
            Destroy(gameObject, anim.GetCurrentAnimatorStateInfo(0).length);    // destroy the enemy after death
        }


    }

    void ApproachPlayer()
    {
        //FacePlayer();
        if (startApproach)
        {
            // find the closest and range point to player, and approach to that point
            float leftRangeToPlayer = Vector3.Distance(attackRange[0], player.transform.position);  // distance to the left range point, default settings
            float rightRangeToPlayer = Vector3.Distance(attackRange[1], player.transform.position);
            // record the closest point
            closetsPos = leftRangeToPlayer - rightRangeToPlayer > 0.2f ? 1 : 0;
            startApproach = false;
        }

        if (isVampire)
        {
            anim.Play("Attack");
            movement.ThrowBomb(player.transform.position);
        }
        else
        {
            anim.Play("Run");
        }

        if (Vector3.Distance(transform.position, attackRange[closetsPos])<0.2f){
            closetsPos = 1 - closetsPos;
        }

        movement.MoveToTarget(attackRange[closetsPos], true);
    }

    void Attack()
    {
        FacePlayer();
        anim.Play("Attack");

        if (isVampire){
            movement.ThrowBomb(player.transform.position);
        }

        Vector3 target = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
        movement.MoveToTarget(target, true);
    }

    void FacePlayer()
    {
        Vector3 relativePos = player.transform.position - transform.position;

        if (relativePos.x > 1)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
        }
        if (relativePos.x < -1)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    void BuildDecisionTree()
    {
        DecisionTree isAliveNode = new DecisionTree();
        isAliveNode.SetDecision(IsAlive);

        DecisionTree isInVisibleNode = new DecisionTree();
        isInVisibleNode.SetDecision(IsVisible);

        DecisionTree isInAttackNode = new DecisionTree();
        isInAttackNode.SetDecision(InAttackRange);

        DecisionTree actPatrolNode = new DecisionTree();
        actPatrolNode.SetAction(Patrol);

        DecisionTree actDieNode = new DecisionTree();
        actDieNode.SetAction(Die);

        DecisionTree actApproachNode = new DecisionTree();
        actApproachNode.SetAction(ApproachPlayer);

        DecisionTree actAttackNode = new DecisionTree();
        actAttackNode.SetAction(Attack);

        isAliveNode.SetLeft(actDieNode);
        isAliveNode.SetRight(isInVisibleNode);

        isInVisibleNode.SetLeft(actPatrolNode);
        isInVisibleNode.SetRight(isInAttackNode);

        isInAttackNode.SetLeft(actApproachNode);
        isInAttackNode.SetRight(actAttackNode);


        root = isAliveNode;
    }

    public void OnCollisionEnter2D(Collision2D collider)
    {

        if (collider.gameObject.tag == "Arrow" && IsAlive())
        {
            Hurt();
            if(transform.localScale.x >0)
            {
                transform.position += new Vector3(-1f, 0, 0);
            }
            else
            {
                transform.position += new Vector3(1f, 0, 0);
            }
            
        }
    }

    private void HideExplosion(){
        explosion.SetActive(false);
    }

    public void OnTriggerEnter2D(Collider2D collider){
        if (Time.time < nextBounceTime){
            return;
        }

        if (collider.gameObject.tag == "Player" || collider.gameObject.tag =="Arrow")
        {
            AudioSource.PlayClipAtPoint(explosionSound, this.transform.position);
            Hurt();
            hitHead = true;
            nextBounceTime = Time.time + 2f;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        hitHead = false;
    }

    private void Hurt(){
        if (explosion.activeSelf == true)
        {
            HideExplosion();
        }
        AudioSource.PlayClipAtPoint(explosionSound, this.transform.position);
        anim.Play("Hurt");
        explosion.SetActive(true);
        health.SetHit(true);

        Invoke("HideExplosion", 0.6f);
        health.TakeDamage();
    }

    public bool HitHead(){
        return hitHead;
    }
}