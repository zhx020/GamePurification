using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bsdisplay : MonoBehaviour {

    int bs;
    public GameObject bs1;
    public GameObject bs2;
    public GameObject bs3;

    // Use this for initialization
    void Start () {
        bs = PlayerPrefs.GetInt("bs");
        if (bs == 1){
            bs1.SetActive(true);
        }
        if (bs == 2)
        {
            bs2.SetActive(true);
        }
        if (bs == 3)
        {
            bs3.SetActive(true);
        }

    }

    // Update is called once per frame
    void Update () {
		
	}
}
