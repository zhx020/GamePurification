using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public float speed = 3f;
    public float attackSpeed = 5f;
    public Transform ground;
    public float patrolRange = 5f;
    public GameObject bomb;

    //public float distance = 2f;
    private List<Vector3> moveRange;
    private float enemyHeight;
    private int currPoint;
    private float next_throw_time;
    private Vector3 originPosition;
    public bool throwHori;  //if the vampire are throwing bomb horizontally in the begining


    private void Awake()
    {
        moveRange = new List<Vector3>();
        enemyHeight = Mathf.Abs(transform.position.y - ground.position.y);
        currPoint = 0;
        originPosition = transform.position;
    }
    private void Start()
    {
        moveRange.Add(new Vector3(transform.position.x - patrolRange, transform.position.y, transform.position.z));
        moveRange.Add(new Vector3(transform.position.x + patrolRange, transform.position.y, transform.position.z));
        next_throw_time = Time.time + 1f;
    }

    public float GetEnemyHeight(){
        return enemyHeight;
    }

    public void Patrol(bool isWalking)
    {
        if (isWalking){
            if (Vector3.Distance(transform.position, moveRange[0]) < 0.2f)
            {
                currPoint = 1;
            }
            else if (Vector3.Distance(transform.position, moveRange[1]) < 0.2f)
            {
                currPoint = 0;
            }
            MoveToTarget(moveRange[currPoint], false);
        }else{
            if (!IsAtOriginPos()){
                MoveToTarget(originPosition, false); 
            }
        }
    }

    public bool IsAtOriginPos(){
        return Vector3.Distance(transform.position, originPosition) < .2f;
    }

    public void ThrowBomb(Vector3 playerPos){
        Vector3 direction = (playerPos - transform.position).normalized;

        if (Time.time > next_throw_time){
            GameObject bulletInstance = Instantiate(bomb, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;

            if(Mathf.Abs(playerPos.y-transform.position.y)<2f || throwHori){
                bulletInstance.GetComponent<Rigidbody2D>().velocity = direction.x < 0 ? new Vector2(-30, 0) : new Vector2(30, 0);
                next_throw_time = 2f + Time.time;
            }
            else{
                next_throw_time = 0.8f + Time.time;
            }
           

            Destroy(bulletInstance, 2f);
        }
    }

    public void MoveToTarget(Vector3 target, bool isAttacking){
        Vector3 direction = (target - transform.position).normalized;
        if (direction.x > 0.1)
        {
            // moving to right
            transform.eulerAngles = new Vector3(0, -180, 0);
        }
       
        if (direction.x <-0.1)
        {
            // moving to left
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        transform.position = isAttacking
            ? (Vector3)Vector2.MoveTowards(transform.position, target, attackSpeed * Time.deltaTime)
            : Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }
}
