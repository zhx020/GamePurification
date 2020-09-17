using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destoryArrow : MonoBehaviour {
    float initialtime;
    public float second;
	// Use this for initialization
	void Start () {
        initialtime = Time.time;
        Invoke("DestroyGameObject", second);
	}
	
	// Update is called once per frame
	void Update () {
		if (initialtime - Time.time > 1.0f)
        {
            Destroy(gameObject);
        }
	}

    void DestroyGameObject()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Destroy(this.gameObject);
        //GameObject col = collision.gameObject;
        //if (col.layer == 0)
        //{
        //    Destroy(this.gameObject);
        //}
    }
}
