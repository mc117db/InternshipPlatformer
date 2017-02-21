using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public float distance = 10;
    public float smoothDuration = 2;
    Vector3 targetPos;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (target)
        {
            Vector3 refVelocity = Vector3.zero;
            targetPos = target.position + Vector3.back * distance;
            gameObject.transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref refVelocity, smoothDuration);
        }
	}
}
