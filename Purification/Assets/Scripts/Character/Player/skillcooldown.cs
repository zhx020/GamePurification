using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class skillcooldown : MonoBehaviour {

    private Image sprite;

    public float CDtime;

    private bool isCDing = false;

	// Use this for initialization
	void Start () {
        sprite = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space) && !isCDing)
        {
            sprite.fillAmount = 1;
            isCDing = true;
            StartCoroutine("CD");
        }
	}

    IEnumerator CD()
    {
        while (true)
        {
            sprite.fillAmount -= Time.deltaTime * (1 / CDtime);
            if (sprite.fillAmount <= 0f)
            {
                isCDing = false;
                break;
            }
            yield return 0;
        }
    }
}
