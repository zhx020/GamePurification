using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healing : MonoBehaviour {

    public AudioClip sound;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(sound, this.transform.position);
            Hearthealth.Instance.AddHP();
            Destroy(gameObject);

        }
    }
}

