using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombCount : MonoBehaviour {
    public int Bomb;
    public Text BombText;

    public static BombCount Instance;
    // Use this for initialization
    void Start () {
        Instance = this; 
        Bomb = PlayerPrefs.GetInt("currentBombs");
    }
	
	// Update is called once per frame
	void Update () {
        BombText.text = Bomb.ToString();

    }
    public void AddBomb(){
        Bomb += 1; 
        PlayerPrefs.SetInt("currentBombs", Bomb);
    }
    public void ConsumeBomb(){
        Bomb -= 1;
        PlayerPrefs.SetInt("currentBombs", Bomb);
    }
}
