using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyActivation : MonoBehaviour {
    public GameObject[] army;
    public GameObject cloud;
    public float detectDistance = 25f;
    private Transform player;
    private float nextActiveTime;
    private float loopCount;    // how many times the army has been activated



	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nextActiveTime = Time.time + 10f;
    }
	
	// Update is called once per frame
	void Update () {
      
        if (Vector3.Distance(player.position, transform.position)< detectDistance 
            && (Time.time > nextActiveTime || System.Math.Abs(loopCount) < 0.2f) && loopCount<=2){


            for (int i = 0; i < army.Length; i++){
                if (army[i] != null){
                    GameObject cloudAppear = Instantiate(cloud, army[i].transform.position, Quaternion.identity) as GameObject;
                    Destroy(cloudAppear, 1f);
                    StartCoroutine(ActiveEnemy(army[i]));
                }else{
                    return;
                }
               
               
            }
            nextActiveTime = Time.time + 6f;
            loopCount++;
        }
	}


    // set enemy to be active
    IEnumerator ActiveEnemy(GameObject enemy){
        yield return new WaitForSeconds(0.8f);
        enemy.SetActive(true);
        enemy.GetComponent<BoxCollider2D>().enabled =true;
        enemy.GetComponent<Renderer>().material.color = Color.gray;
        enemy.GetComponent<EnemyHealth>().SetHealth();

    }
}
