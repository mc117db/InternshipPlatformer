using UnityEngine;
using System.Collections;
using System;

public class EnemyAimBehaviour : MonoBehaviour, IWielder, IRecyle {
    public IFirable weaponComponent; // Call Fire() to fire the equipped weapon
    public GameObject weapon; // Weapon should have a gun component (in this context) but as long as the component
                              // implements IFirable then its up for use.
    public GameObject aimController;
    public Transform target;
    [Space(10)]
    public int shootAmountPerInterval = 3;
    public float waitTimeBetweenShots = 0.5f;
    public float waitTimeBetweenIntervals = 3f;
    private float lineOfSightUpdateRate = 0.2f; // We dont need to do a LoS check every frame do we?

    private bool canFire = true;
    private bool canSeeTarget = false;
    private bool isActive;

    // Pooling methods
    public void Restart()
    {
        if (!target)
        {
            if (GameObject.Find("Player"))
            {
                target = GameObject.Find("Player").transform;
            }
            if (!target)
            {
                isActive = false;
            }
            else
            {
                isActive = true;
            }
        }
        else if (target)
        {
            // Start Line of Sight check
            InvokeRepeating("isTargetInLineOfSight", 0, lineOfSightUpdateRate);
            isActive = true;
        }
    }

    public void Shutdown()
    {
        CancelInvoke();
        target = null;
    }

    public Vector3 returnAimPosition()
    {
        // This is in screen space;
        if (!target)
        {
           // Debug.Log("NO TARGET!");
            return Vector3.zero;
        }
        else
        {
            return target.position - new Vector3(0,0.5f,0);
        }
    }


    void isTargetInLineOfSight()
    {
        if (target)
        {
            RaycastHit hit;
            if (Physics.Linecast(transform.position, target.position, out hit))
            {
                if (hit.collider.gameObject.transform == target)
                {
                    canSeeTarget = true;
                }
                else
                {
                    canSeeTarget = false;
                }
            }
        }
    }
  

    // Use this for initialization
    void Start () {
        FindDependecies();
        FetchWeaponBehaviorsInternally();
        Restart();
	}

    void FindDependecies()
    {
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
    }
	void FetchWeaponBehaviorsInternally()
    {
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
	    if (canFire && canSeeTarget && isActive)
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
