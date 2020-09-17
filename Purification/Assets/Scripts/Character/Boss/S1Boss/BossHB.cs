using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHB : MonoBehaviour {

    private bool Wudi = false;
    public bool hit = false;
    public static BossHB Instance;
    public GameObject hurtBlood;
    private float Timer;
    private float attackTimer;
    Collider2D m_Collider;
    // Use this for initialization
    void Start()
    {
        Instance = this;
        m_Collider = GetComponent<Collider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        attackTimer += Time.deltaTime;
        if(attackTimer > 5)
        {
            m_Collider.enabled = true;
        }
        if(hurtBlood.activeSelf == true)
        {
            Timer += Time.deltaTime;
            if(Timer >1)
            {
                hurtBlood.SetActive(false);
                Timer = 0;
            }
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        
        if (col.gameObject.tag == "Arrow"&& Wudi == false)
        {
            hurtBlood.SetActive(true);
            BossHP.Instance.Hp -= 3;
            Destroy(col.gameObject);
            
        }
        if(col.gameObject.tag =="Player")
        {
            hit = true;
        }
    }  
}
