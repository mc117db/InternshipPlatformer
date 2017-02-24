using UnityEngine;
using System.Collections;
using System;

public class EnemyAimBehaviour : MonoBehaviour, IWielder {
    // Note to self: There is a soft coupling between EnemyAimBehaviour and GunBehaviour
    public IFirable weaponComponent; // Call Fire() to fire the equipped weapon
    public GameObject weapon; // Weapon should have a gun component (in this context) but as long as the component
                              // implements IFirable then its up for use.
    public GameObject aimController;
    public Transform target;
    [Space(10)]
    public int shootAmountPerInterval = 3;
    public float waitTimeBetweenShots = 0.5f;
    public float waitTimeBetweenIntervals = 3f;

    private bool canFire = true;


    public Vector3 returnAimPosition()
    {
        // This is in screen space;
        if (!target)
        {
            Debug.Log("NO TARGET!");
            return Vector3.zero;
        }
        else
        {
            return target.position - new Vector3(0,0.5f,0);
        }
    }

    /*
    public bool isTargetInLineOfSight(Transform iTarget)
    {
        // Do a raycast towards the target's direction, if hit the target return true
        RaycastHit[] hit = new RaycastHit[0];
        Ray d_ray = new Ray();
        d_ray.direction = iTarget.position - transform.position;
        d_ray.origin = transform.position;

        if (Physics.Raycast(d_ray,hit))
        {
            if (hit.transform == iTarget)
            {
                // enemy can see the player!
            }
            else
            {
                // there is something obstructing the view
            }
        }
    }
    */

    // Use this for initialization
    void Start () {
        if (!target)
        {
            target = GameObject.Find("Player").transform;
        }
        if (!weapon)
        {
            Debug.Log(gameObject.name + "has no weapon assigned to it!");
            return;
        }
        if (!aimController)
        {
            Debug.Log(gameObject.name + "has no Aim Controller assigned to it!");
            return;
        }

        var components = weapon.GetComponents<MonoBehaviour>();
        foreach (var component in components)
        {
            if (component is IFirable)
            {
                // Return back the weapon component
                weaponComponent = (IFirable)component;
                //.Log("Found component");
            }
        }

        var aimComponents = aimController.GetComponents<MonoBehaviour>();
        foreach (var component in aimComponents)
        {
            if (component is IWieldable)
            {
                IWieldable wieldableComponent = (IWieldable)component;
                wieldableComponent.SetWielder((IWielder)this);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	    if (canFire)
        {
            StartCoroutine(ExecuteShootInterval());   
        }
	}
    IEnumerator ExecuteShootInterval()
    {
        canFire = false;
        for (int i = 0; i < shootAmountPerInterval; i++)
        {
            //Debug.Log("BANG!");
            Shoot();
            yield return new WaitForSeconds(waitTimeBetweenShots);
        }
        StartCoroutine(CooldownBetweenInterval());
    }
    IEnumerator CooldownBetweenInterval()
    {
        yield return new WaitForSeconds(waitTimeBetweenIntervals);
        canFire = true;
    }
    void Shoot()
    {
        if(weaponComponent != null)
        {
            weaponComponent.Fire();
        }
    }
}
