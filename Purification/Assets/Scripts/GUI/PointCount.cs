using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PointCount : MonoBehaviour {
    public int Score;
    public Text ScoreText;

    public static PointCount Instance;
    int enemycount;


    // Use this for initialization
    private void Awake()
    { 
    }
    void Start () {
        Instance = this; ////指定Instance這個程式
        Score = PlayerPrefs.GetInt("currentscore");
    }

    // Update is called once per frame
    void Update () {
        ScoreText.text =  Score.ToString();

    }
    public void AddScore()
    {

        Score += 10; //分數+10
        PlayerPrefs.SetInt("currentscore",Score);


        // 更改ScoreText的內容

    }
    public void AddEnemyKill(int point)
    {
        Score += point;
        Score = PlayerPrefs.GetInt("currentscore");
    }


}
