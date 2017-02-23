using UnityEngine;
using System.Collections;

public class GunBehaviour : MonoBehaviour {

    public Transform wielder;
    public Transform FiringPoint;
    private Transform gun;
    private Vector3 aimPosition;
    private float angle;
    private Camera cam;

    private bool isAimingRight;

    // Use this for initialization
    void Start () {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        gun = transform.GetChild(0);
        // Try to find missing references
        if (FiringPoint == null)
        {
            transform.Find("FiringPoint");
        }
        if (wielder == null)
        {
            wielder = transform.parent;
            wielder.transform.localPosition = Vector3.zero;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (!wielder && cam)
        {
            return;
        }

        // Get the aim direction and normalise the vector
        aimPosition = Input.mousePosition;
        Vector3 wielderScreenPos = cam.WorldToScreenPoint(wielder.position);
        float xDiff = aimPosition.x - wielderScreenPos.x;
        float yDiff = aimPosition.y - wielderScreenPos.y;

        // Get the angle
        angle = Mathf.Atan2(yDiff, xDiff) * Mathf.Rad2Deg;

        // Rotate the gun in the Z axis
        transform.eulerAngles = new Vector3 (0,0,angle);

        if (angle > 0f && angle < 100f || angle < 0f && angle > -90f)
        {

            if (isAimingRight)
            {
                isAimingRight = false;
                gun.localScale = new Vector3(1, 1, 1);
                gun.localRotation = new Quaternion(0, 0, 0, 1);
                FiringPoint.transform.localEulerAngles = new Vector3(0,0,0);
            }
        }

        else if (angle > 100f && angle < 180f || angle < -90f && angle > -180f)
        {
            if (!isAimingRight)
            {
                isAimingRight = true;
                gun.localScale = new Vector3(-1, 1, 1);
                gun.localRotation = new Quaternion(0, 0, 1, 0);
                FiringPoint.transform.localEulerAngles = new Vector3(0, 0, 180);
            }
       
        }

    }
}
