using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarDisplay : MonoBehaviour {

    public Sprite noStar;
    public Sprite twoStar;
    public Sprite fullStar;
    public Image stars;

    public Text scoreText;
    public int score;

    // Use this for initialization
    void Start () {
        int.TryParse(scoreText.text, out score); 
        if (score >=6000){
            stars.sprite = fullStar;
        }else if(score >=600 && score<6000){
            stars.sprite = twoStar;
        }else{
            stars.sprite = noStar;
        }
	}
}
