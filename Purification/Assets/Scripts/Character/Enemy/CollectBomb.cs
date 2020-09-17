using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectBomb : MonoBehaviour {

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            BombCount.Instance.AddBomb();

        }
    }
}
