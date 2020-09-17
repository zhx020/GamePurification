using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    private float startHealth;
    private float currentHealth;                   // The current health the enemy has.
    private bool a_damage;      // boolean to check if it is able to take the damage from ally
    public float damageAmount;
    public bool hit = false;        // boolean to check if the enemy are avaible to take damage
    public static EnemyHealth Instance;
    public GameObject hurtBlood;

    void Awake()
    {
        startHealth = PlayerPrefs.GetInt("Startinghealth");
        SetHealth();
        Instance = this;
        a_damage = false;
    }

    public void SetHealth(){
        currentHealth = startHealth;
        if (currentHealth < 0.2f)
        {
            currentHealth = 60;
        }
    }

    public void TakeDamage()
    {
        if(hit == true)
        {
            currentHealth -= damageAmount;
            hit = false;
        }
    }

    public void SetHit(bool hit){
        this.hit = hit;   
    }

    public bool IsAlive(){
        return currentHealth > 0;
    }

    public void InvokeAllyDamage(){
        a_damage = true;
      
    }

    public void HurtByAlly(){
        hurtBlood.SetActive(true);
        if (a_damage){
           
            currentHealth -= 1f;
            a_damage = false;
        }
        Invoke("InvokeAllyDamage", 2);
    }


    public void AllyLeft(){
        hurtBlood.SetActive(false);
    }
}
