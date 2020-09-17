using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentScore : MonoBehaviour {
    public Text scoreText;
    public Text numOfCoins;

    // Use this for initialization
    void Start () {
        scoreText.text = numOfCoins.text;
    }
}
