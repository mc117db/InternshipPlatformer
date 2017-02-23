using UnityEngine;
using System.Collections;

public interface IWielder
{
    Vector3 returnAimPosition();
}
public interface IWieldable
{
    void SetWielder(IWielder wielderB);
}
public class GunBehaviour : MonoBehaviour,IWieldable {
    // Note to self: There is a soft coupling between EnemyAimBehaviour and GunBehaviour
    public Transform wielder;
    public Transform FiringPoint;
    private Transform gun;
    private Vector3 aimPosition;
    private float angle;
    private Camera cam;
    public IWielder wielderBehaviour;

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
    public void SetWielder(IWielder wielderB)
    {
        wielderBehaviour = wielderB;
    }
	void Update () {
        if (!wielder && cam)
        {
            return;
        }

        // Get the aim direction and normalise the vector
        if (wielderBehaviour == null)
        {
            aimPosition = Input.mousePosition; // Need to encapsulate this...
        }
        else
        {
            // If there is a wielder behaviour attached, get that instead.
            aimPosition = cam.WorldToScreenPoint(wielderBehaviour.returnAimPosition());
        }

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
