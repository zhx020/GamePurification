using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHP : MonoBehaviour {
    public float MaxHp;
    public float Hp;
    private Image Pic;
    public bool Q3 = false;
    public bool Q2 = false;
    public bool Q1 = false;
    public static BossHP Instance;

    // Use this for initialization
    void Start()
    {
        Instance = this;
        Pic = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localPosition = new Vector3((-28 + 28 * (Hp / MaxHp)), 0.0f, 0.0f);
        if(Hp <= 70)
        {
            Pic.color = Color.grey;
            Q3 = true;
            Q2 = false;
            Q1 = false;
        }
        if(Hp<= 30)
        {
            Pic.color = Color.red;
            Q3 = false;
            Q2 = true;
            Q1 = false;
        }
        if(Hp <= 10)
        {
            Q3 = false;
            Q2 = false;
            Q1 = true;
        }
    }
}
