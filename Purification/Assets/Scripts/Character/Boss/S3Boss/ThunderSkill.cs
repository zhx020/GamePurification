using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderSkill : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    private GameObject knifePF;

    private GameObject Player;

    private Vector3 direction;


    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        InvokeRepeating("LaunchKnife", 0.0f, 3f);

    }
    void Update()
    {
        direction = new Vector3(Random.Range(Player.transform.position.x-10,Player.transform.position.x+10),transform.position.y,transform.position.z);
    }
    void LaunchKnife()
    {
        if (Player.activeSelf)
        {
            Instantiate(knifePF, direction, transform.rotation);
        }

    }
}
