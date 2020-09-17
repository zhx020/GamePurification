using UnityEngine;

public class SmallKnife : MonoBehaviour {


    private Vector3 direction;
    private GameObject player;
    private float MoveSpeed = 20f;
    private float Timer;

    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
       
            direction = (player.transform.position - transform.position).normalized;

            Vector3 dir = player.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 190;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
       


    }

    // Update is called once per frame
    void Update () {

       
        AttackPlayer();
        Timer += Time.deltaTime;
        if(Timer>15)
        {
            Destroy(this.gameObject);
        }
    }
    void AttackPlayer()
    {
        transform.position += direction.normalized * MoveSpeed  * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag != "Enemy_Skill")
        {
            Destroy(this.gameObject);
        }
        
        
    }
}
