using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour {

    public float shakeAmount;
    public Vector2 ShakePos;
    private Vector3 origin;
    // Use this for initialization
    void Start () {
        origin = new Vector3(0, 0, -10);
	}
	
	// Update is called once per frame
	void Update () {
       ShakePos = Random.insideUnitCircle * shakeAmount;

        transform.position = Vector3.Lerp(transform.position,origin, Time.deltaTime * 2);

    }



    public void screenShake()
    {
       
        transform.position = new Vector3(transform.position.x + ShakePos.x, transform.position.y + ShakePos.y, -10);


    }
}
