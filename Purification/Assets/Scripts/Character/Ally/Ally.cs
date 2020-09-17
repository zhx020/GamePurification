using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : MonoBehaviour
{

    public float alertRadius = 5f;
    public float ToPlayerMaxDis;
    public float enemydetectradius;
   
    private float distanceToEnemy;
    private GameObject enemy;
    private GameObject lastAttackEnemy;
    private GameObject hero;
    private AllyBehave allyBehave;
    private AllayState CUR_STATE;

    // debug
    private float startRecord;
    private float debugTime;
    private Vector3 lastPos;

    void Awake()
    {
        allyBehave = GetComponent<AllyBehave>();
        hero = GameObject.FindWithTag("Player");
        CUR_STATE = AllayState.STANDBY;
        debugTime = 1f;
        startRecord = Time.time;
        lastPos = transform.position;
    }

    void FixedUpdate()
    {
        // debug if the ally is at the same position for 2 seconds
        if (Time.time > startRecord + debugTime){
            startRecord = Time.time;

            if (Mathf.Abs(transform.position.x - lastPos.x)< 0.02f){
                CUR_STATE = AllayState.STANDBY;
                return;
            }else{
                lastPos = transform.position;
            }

        }

        // instantiate enemy and lastAttackEnmemy
        if (hero.GetComponent<PlayerController>().IsOnGround()){
            enemy = allyBehave.FindClosestEnemy(hero);
        }
       
        // if ally is having a bug, restart it
        if (allyBehave.IsRestarted()){
            CUR_STATE = AllayState.STANDBY;
            transform.position = Vector3.MoveTowards(transform.position, allyBehave.TransferPostion(hero), Mathf.Infinity);
            return;
        }

        // check if the allly is in the the camera view
        // if not, transfer it to player position, swithch state to following
        if (!IsInView(transform.position))
        {
            // if the player is alive, transfer its to the player's position
            transform.position = Vector3.MoveTowards(transform.position, allyBehave.TransferPostion(hero), Mathf.Infinity);
            if (lastAttackEnemy!= null && lastAttackEnemy.GetComponent<EnemyHealth>().IsAlive())
                lastAttackEnemy.GetComponent<EnemyHealth>().AllyLeft();

            CUR_STATE = AllayState.STANDBY;
        }

        // state machine
        switch (CUR_STATE)
        {
            case AllayState.STANDBY:

                allyBehave.WaitForPlayer();

                if (IsPlayerClosed() && hero.activeSelf)
                {
                   
                    allyBehave.SwitchTarget(hero, true);
                    CUR_STATE = AllayState.FOLLOW;
                    
                }else if(!hero.activeSelf){
                    enemy.GetComponent<EnemyHealth>().AllyLeft();
                }

                break;

            case AllayState.FOLLOW:
                // if the ally is already finished the swtichTarget process, move towards player; else, continue
                if (allyBehave.IsOnNextPoint()){

                    allyBehave.MoveToTarget(hero, true);
                }else{
                    allyBehave.MoveToNextPoint();
                }


                // if the enemy is closed and the enemy is in the camera range, attack towards it
                if (enemy != null && IsEnemyClosed() && IsInView(enemy.transform.position) && !allyBehave.IsAllyJumping())
                {
                
                    allyBehave.SwitchTarget(enemy, false);
                    CUR_STATE = AllayState.TOWARDENEMY;
                   
                }
                break;

            case AllayState.TOWARDENEMY:

              
                // check if the current targeting enemy is the same as last time
                if (enemy != null && lastAttackEnemy != null && enemy.name != lastAttackEnemy.name)
                {
                    lastAttackEnemy.GetComponent<EnemyHealth>().AllyLeft();
                    lastAttackEnemy = enemy;
                    allyBehave.SwitchTarget(enemy, false);
                }


                // if the player has gone far away or the attacking enemy is dead, move towards player
                if (!allyBehave.IsAllyJumping() && (CheckIfPlayerLongGone() || lastAttackEnemy==null))
                {
                    if (CheckIfPlayerLongGone())
                    {
                        enemy.GetComponent<EnemyHealth>().AllyLeft();
                    }
                    lastAttackEnemy = enemy;
                    allyBehave.SwitchTarget(hero, true);
                    CUR_STATE = AllayState.FOLLOW;

                    break;
                }

                if (!hero.activeSelf){
                    CUR_STATE = AllayState.STANDBY;
                    break;
                }

              
                // if the ally is already finished the swtichTarget process, move towards enemy; else, continue
                if (allyBehave.IsOnNextPoint()){
        
                    allyBehave.MoveToTarget(enemy, false);
                }else{
          
                    allyBehave.MoveToNextPoint();

                }

                break;
        }
    }

    private bool IsInView(Vector3 worldPos){
        Transform camTransform = Camera.main.transform;
        Vector2 viewPos = Camera.main.WorldToViewportPoint(worldPos);

        return viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 ? true : false;
    }

    private bool CheckIfPlayerLongGone()
    {
        return Vector3.Distance(hero.transform.position, transform.position) > ToPlayerMaxDis;
    }


    private bool IsPlayerClosed()
    {
        float disToPlayer = Vector3.Distance(transform.position, hero.transform.position);
        return disToPlayer < alertRadius;
    }

    private bool IsEnemyClosed()
    {
        distanceToEnemy = Vector2.Distance(enemy.transform.position, hero.transform.position);
        return (distanceToEnemy < enemydetectradius);
    }

    private enum AllayState
    {
        STANDBY,
        FOLLOW,
        TOWARDENEMY,
    }
}