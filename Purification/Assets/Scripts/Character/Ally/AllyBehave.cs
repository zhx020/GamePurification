using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyBehave : MonoBehaviour
{
    public Animator anim;
    public float moveSpeed = 20f;
    public GameObject ground;

    // Movement instances
    private Map                 map;
    private List<GameObject>    shortest_path;
    private Transform           lastEndPoint;
    private float               allayHeight;
    private Vector3             allay_pos; 
    private Vector3             prev_target; //previous targeted node in the pathfinding
    private Vector3             curr_target; //current targeted node in the pathfinding
    private bool                isJumping;     // is ally jumping
    private Rigidbody2D         rb;
    private float               gravity;
    private float               initialAngle;
    private Vector3             switchNextPoint;    // the closest point to the ally when it swithes state
    private bool                onNextPoint;    // true if the ally arrives the switchNextPoint
   
    // prevent bugs
    private float               startTime;  // start time of moving from one point to another
    private bool                restart;    // if ally is having a bug, restart it
    private float               startMoveTo;  //start time of moving towards a target horizontally
    private float               debugInterval=50;    // the time interval for ally to check if it arrives another place in time
  



    // Use this for initialization
    void Start()
    {
        allay_pos       = transform.position;
        allayHeight     = Mathf.Abs(transform.position.y-ground.transform.position.y);
        map             = (Map)ScriptableObject.CreateInstance(typeof(Map));
        rb              = GetComponent<Rigidbody2D>();
        gravity         = Physics.gravity.magnitude;
        initialAngle    = 80;
        onNextPoint     = true;
    }

    private void UpdatePosition(){
        allay_pos = transform.position;
    }

    public bool IsAllyJumping(){
        return isJumping;
    }
    public void FindNewPath(GameObject target){
        UpdatePosition();
        shortest_path = map.Shortest_path(map.FindClosestPoint(gameObject, allayHeight), 
                                          map.FindClosestPoint(target, allayHeight));
        // if the player is not at the lastEndPoint, update lastEndPoint
        lastEndPoint = (shortest_path!=null && shortest_path.Count != 0) ? shortest_path[shortest_path.Count - 1].transform : null;

    }

    public void MoveToTarget(GameObject target, bool moveToPlayer)
    {
        // if ally is having a bug in jumping process
        if ( Time.time > startTime + debugInterval)
        {
            restart = true;
            isJumping = false;
            return;
        }

        restart = false;
        // update the position of allay and the hero
        UpdatePosition();
        Vector3 allayFootPos = new Vector3(allay_pos.x, allay_pos.y - allayHeight, allay_pos.z);

        // if allay reached the endpoint from last shortest path?
        // yes->do following; no->walk along the shortest path
        if (lastEndPoint != null && Vector3.Distance(allayFootPos, lastEndPoint.position) < .5f)
        {
            isJumping = false;
            // update previous target for deciding the needs for jumping
            prev_target = lastEndPoint.position;
            shortest_path = map.Shortest_path(map.FindClosestPoint(gameObject, allayHeight), 
                                              map.FindClosestPoint(target, allayHeight));

     
            // if the player is not at the lastEndPoint, update lastEndPoint
            if (shortest_path !=null && shortest_path.Count != 0){
                lastEndPoint = shortest_path[shortest_path.Count - 1].transform;
                curr_target = lastEndPoint.position;

                // record the startTime from this point
                startTime = Time.time;
                startMoveTo = 0;
                // calculate the horizontal distance between two nodes
                if (curr_target.y - prev_target.y >1f){
                    Jump(4);
                    anim.Play("Jump");
                   
                    isJumping = true;
                }
                else if(prev_target.y - curr_target.y > 1f){
                    Jump(3);
                    anim.Play("Jump");

                    isJumping = true;
                }

            }else{
                lastEndPoint = null;
            }
        }else if (lastEndPoint == null){
            isJumping = false;
            startMoveTo += Time.deltaTime;
            // if reaches the closest point to the target and is too far to reach it, move towards the target
            if (Vector3.Distance(target.transform.position, transform.position) > 2.5f 
                && Mathf.Abs(target.transform.position.y - transform.position.y) < 2f)
            {
              
                // debug
                if (startMoveTo>debugInterval){
                    isJumping = false;
                    restart = true;
                    return;
                }

                Vector3 tartgetFoot = new Vector3(target.transform.position.x, 
                                                  target.transform.position.y - allayHeight, 
                                                  target.transform.position.z);
                if (moveToPlayer){
                   
                    MoveAlongPath(tartgetFoot, false, true);
                }else{
                    MoveAlongPath(tartgetFoot, true, true);
                }
            }else if (Mathf.Abs(target.transform.position.y-transform.position.y)<2f){
               
                // if it is closed enough to the target, idles if the tartget is player, attack if it is enemy
                if (moveToPlayer)
                {
                    anim.Play("Idle");

                }
                else
                {
                    anim.Play("RunSlash");
                    target.GetComponent<EnemyHealth>().HurtByAlly();
                }
            }

            if (moveToPlayer){
                FindNewPath(target);
                if (lastEndPoint != null)
                {
                    if(Mathf.Abs(lastEndPoint.position.y - (transform.position.y - allayHeight)) >1f){
                        SwitchTarget(target, moveToPlayer);
                        MoveToNextPoint();
                    }
                }
            }
        }
        else{
            // if the ally is still on the way to target
            if (moveToPlayer){
       
                MoveAlongPath(lastEndPoint.position, false, false);   // if the final target is play, run towards palyer
            }else{
                MoveAlongPath(lastEndPoint.position, true, false);    // if the target is enemy, run and slash towards enemy
            }
        }
    }

    private void SwitchToFollow(){

        Vector3 faceDirection = (switchNextPoint - transform.position);
        if(Mathf.Abs(faceDirection.x) > 1){
            // adding buffer to change the direction
            transform.rotation = Quaternion.LookRotation(faceDirection.normalized);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 90, 0);
        }
   }

    public void SwitchTarget(GameObject target, bool moveToPlayer){
        FindNewPath(target);
        // this is called when the ally  was out of the screen and transfered to the player's position
       
        if (lastEndPoint==null && moveToPlayer){
            onNextPoint = true;
            if (Vector3.Distance(target.transform.position, transform.position)<2f){
                anim.Play("Idle");      
            }else{
                anim.Play("Run");
            }
        }
        else{
            Vector3 closet_point = map.FindClosestPoint(gameObject, allayHeight).transform.position;
            switchNextPoint = new Vector3(closet_point.x, closet_point.y + allayHeight, closet_point.z);
            onNextPoint = false;

             // record the startTime from this point
            startTime = Time.time;

            SwitchToFollow();
        }
    }

    //if ally has not reached to the switchNextpoint, move towards
    public void MoveToNextPoint(){
        // if ally is having a bug in jumping process
        if (Time.time > startTime + debugInterval)
        {
            restart = true;
            isJumping = false;
            return;
        }

        restart = false;

        if (Vector3.Distance(transform.position, switchNextPoint) > 0.5 && !onNextPoint)
        {
            anim.Play("Run");
            transform.position = Vector3.MoveTowards(transform.position, switchNextPoint, Time.deltaTime * moveSpeed);
        }else{
            // turn bool to true since the ally arrives the switchNextPoint
            onNextPoint = true;

            // when ally arrives closest point, check the height between this point and the lastEndPoint if exists
            if (lastEndPoint != null){
               
                FirstJump();
            }
        }
    }

    private void FirstJump(){
        Vector3 node_pos = lastEndPoint.transform.position;
        Vector3 destination = new Vector3(node_pos.x, node_pos.y + allayHeight, node_pos.z);

        // jump if the current position is higher or lower than the last end point
        isJumping = false;

        if (destination.y - transform.position.y > 2f)
        {
            isJumping = true;
            anim.Play("Jump");
            Jump(4);
        }
        else if (transform.position.y - destination.y > 2f)
        {
            isJumping = true;
            anim.Play("Jump");
            Jump(3);
        }
        transform.position = Vector3.MoveTowards(transform.position, lastEndPoint.position, Time.deltaTime * moveSpeed);
    }

    private void Jump(int v_speed){
        Vector3 node_pos = lastEndPoint.transform.position;
        Vector3 destination = new Vector3(node_pos.x, node_pos.y + allayHeight, node_pos.z);

        float angle = initialAngle * Mathf.Deg2Rad;
        Vector3 planarTarget = new Vector3(destination.x, 0, destination.z);
        Vector3 planarPostion = new Vector3(transform.position.x, 0, transform.position.z);

        // Planar distance between objects
        float distance = Vector3.Distance(planarTarget, planarPostion);
        // Distance along the y axis between objects
        float yOffset = transform.position.y - destination.y;

        float initialVelocity = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / ( distance * Mathf.Tan(angle) + yOffset));
 
        Vector3 velocity = new Vector3(initialVelocity * Mathf.Cos(angle)*2, initialVelocity * Mathf.Sin(angle)* v_speed, 0);

        // Rotate our velocity to match the direction between the two objects
        float angleBetweenObjects = Vector3.Angle(Vector3.right, planarTarget - planarPostion) * (destination.x > transform.position.x ? 1 : -1);
        Vector3 finalVelocity = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * velocity;

        if (!float.IsNaN(finalVelocity.x) && !float.IsNaN(finalVelocity.y) && !float.IsNaN(finalVelocity.z))
        {
            rb.velocity = finalVelocity;
        }
    }

    private void MoveAlongPath(Vector3 node_pos, bool attack, bool notPathFinding){
        Vector3 destination = new Vector3(node_pos.x, node_pos.y + allayHeight, node_pos.z);

        if (notPathFinding){
          
            destination = new Vector3(node_pos.x - 2, node_pos.y + allayHeight, node_pos.z);
        }

        Vector3 faceDirection = (node_pos - allay_pos);
        if (Mathf.Abs(faceDirection.x)>1){
            transform.rotation = Quaternion.LookRotation(faceDirection.normalized);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 90, 0);
        }
       
        if (attack){
            anim.Play("RunSlash");
        }else{
            anim.Play("Run");
        }
        transform.position = Vector3.MoveTowards(allay_pos, destination, Time.deltaTime * moveSpeed);
    }

    public Vector3 TransferPostion(GameObject hero){

        Vector3 heroPos = map.FindClosestPoint(hero, allayHeight).transform.position;
        Vector3 destination = new Vector3(heroPos.x, heroPos.y + allayHeight, heroPos.z);
        return destination;
    }

    public GameObject FindClosestEnemy(GameObject hero)
    {
        // find the closest enemy to the player
        UpdatePosition();
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;

        foreach (GameObject go in gos)
        {

            Vector3 diff = go.transform.position - hero.transform.position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    public void WaitForPlayer(){
        anim.Play("Idle");
    }

    public bool IsOnNextPoint(){
        return onNextPoint;
    }

    public bool IsRestarted(){
        return restart;
    }
}


