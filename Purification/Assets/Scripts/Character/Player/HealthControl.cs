using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthControl : MonoBehaviour
{
    public float MaxHP = 100;

    public float HP = 100;
    
    public static HealthControl Instance;
    // Use this for initialization
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localPosition = new Vector3(-1400 + 1400 * (HP / MaxHP), 0.0f, 0.0f);
    }
    public void LossHP()
    {
        if (HP> 0)
        {
            HP = HP -20;
        }
       
    }
    public void AddHP()
    {
        if (HP <= 80)
        {
            HP = HP + 20;
        }
        if(HP > 80)
        {
            HP = MaxHP;
        }

    }
}
