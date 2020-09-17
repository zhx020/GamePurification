using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    [SerializeField]
    private GameObject coin;

	// Use this for initialization
	void Start () {
        
        for (int y = 0; y < 20; y++)
        {

            Vector3 position = new Vector3(Random.Range(transform.position.x - 10, transform.position.x + 10), Random.Range(transform.position.y-5,transform.position.y), transform.position.z);
            Instantiate(coin, position, transform.rotation);
            
        }
    }
	

	
}
