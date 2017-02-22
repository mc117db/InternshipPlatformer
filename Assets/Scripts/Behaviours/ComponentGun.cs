using UnityEngine;
using System.Collections;

public class ComponentGun : MonoBehaviour {

    Transform FiringPoint;
    public GameObject bulletprefab;
    public float bulletSpeed = 10;
    public int bulletDamage = 1;
    public float rateOfFire = 0.5f;
    bool canFire = true;
    float timeToNextFire;

	// Use this for initialization
	void Start () {
        if (!FiringPoint)
        {
            if (transform.Find("FiringPoint") != null)
            {
                FiringPoint = transform.Find("FiringPoint");
            }
            else
            {
                Debug.Log(gameObject.name + " is lacking a FiringPoint empty gameobject to reference from!");
                // Tips for making a good firing point, align the X axis (RED ARROW), towards the direction you want to face in your gun.
                // FiringPoint must be an empty gameobject
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetMouseButton(0))
        {
            if (canFire)
            {
                Fire();
                timeToNextFire = rateOfFire;
                canFire = false;
            }
        }
        if (!canFire)
        {
            timeToNextFire -= Time.deltaTime;
            if (timeToNextFire <= 0)
            {
                canFire = true;
            }
        }
    }

    void Fire()
    {
        var bullet = GameObjectUtil.Instantiate(bulletprefab, FiringPoint.position);
        var bulletComponent = bullet.GetComponent<ComponentBullet>();
        bulletComponent.SetBulletValue(FiringPoint.right, bulletSpeed, bulletDamage);
    }
}
