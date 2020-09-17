using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followingcamera : MonoBehaviour {

    public float xMargin = 1f;
    public float yMargin = 1f;
    public float xSmooth = 8f;
    public float ySmooth = 8f;
    public Vector2 maxXY;
    public Vector2 minXY;

    private Transform player;

	void Awake () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
    bool checkXMargin()
    {
        return Mathf.Abs(transform.position.x - player.position.x) > xMargin;
    }

    bool checkYMargin()
    {
        return Mathf.Abs(transform.position.y - player.position.y) > yMargin;
    }

    void FixedUpdate () {
        TrackPlayer();
	}

    void TrackPlayer()
    {
        float targetX = transform.position.x;
        float targetY = transform.position.y;
        if (checkXMargin())
        {
            targetX = Mathf.Lerp(transform.position.x, player.position.x, xSmooth * Time.deltaTime);
        }
        if (checkYMargin())
        {
            targetY = Mathf.Lerp(transform.position.y, player.position.y, ySmooth * Time.deltaTime);
        }
        targetX = Mathf.Clamp(targetX, minXY.x, maxXY.x);
        targetY = Mathf.Clamp(targetY, minXY.y, maxXY.y);

        transform.position = new Vector3(targetX, targetY, transform.position.z);
    }
}
