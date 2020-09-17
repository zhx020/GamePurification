using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hearthealth : MonoBehaviour
{
    public PlayerController player;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public static Hearthealth Instance;

    public int health;
    public int numOfHearts;

    private void Awake()
    {
    }

    void Start()
    {
        Instance = this;
        player = GetComponent<PlayerController>();
        health  = PlayerPrefs.GetInt("currentHealth");
    }

    void Update()
    {
        if (health > numOfHearts)
        {
            health = numOfHearts;
            PlayerPrefs.SetInt("currentHealth", health);

        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }

        }

    }

    public void LossHP()
    {
        if (health > 0)
        {
            health -= 1;
            PlayerPrefs.SetInt("currentHealth", health);

        }

    }
    public void AddHP()
    {
        if (health > numOfHearts)
        {
            health = numOfHearts;
            PlayerPrefs.SetInt("currentHealth", health);
        }
        else
        {
            health += 1;
            PlayerPrefs.SetInt("currentHealth", health);
        }

    }
    public void TeammateSkill()
    {
        if (health > numOfHearts)
        {
            health = numOfHearts;
            PlayerPrefs.SetInt("currentHealth", health);

        }
        else
        {
            health += 3;
            PlayerPrefs.SetInt("currentHealth", health);

        }

    }

    public bool IsAlive(){
        return health > 0;
    }
}



