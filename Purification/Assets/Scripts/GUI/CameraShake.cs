using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

    private bool cancelShake = false;
    public bool IfShake;
    public static CameraShake Instance;

    private void Start()
    {
        Instance = this;
    }
    private void Update()
    {
        
        
        if(IfShake == true)
        { 
            StartCoroutine(ShakeCamera());
        }
    }

    /*
     * 震动摄像机
     * shakeStrength ->震动幅度
     * rate -> 震动频率
     * shakeTime -> 震动时长
     */
    IEnumerator ShakeCamera(float shakeStrength = 0.4f, float rate =24, float shakeTime = 0.5f)
    {
        float t = 0;   
        float speed = 1 / shakeTime;   
        Vector3 orgPosition = transform.localPosition; 

        while (t < 1 && !cancelShake)
        {
            t += Time.deltaTime * speed;
            transform.position = orgPosition + new Vector3(Mathf.Sin(rate * t), Mathf.Cos(rate * t), 0) * Mathf.Lerp(shakeStrength, 0, t);
            yield return null;
        }
        cancelShake = false;
        transform.position = orgPosition;
        IfShake = false;
    }
}
