using UnityEngine;
using System.Collections;
using System;

public class EnemyAimBehaviour : MonoBehaviour, IWielder {
    // Note to self: There is a soft coupling between EnemyAimBehaviour and GunBehaviour
    public IFirable weaponComponent;
    public GameObject weapon; // Weapon should have a gun component (in this context) but as long as the component
                              // implements IFirable then its up for use.
    public GameObject aimController;
    public Transform target;

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
                Debug.Log("Found component");
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
	
	}
}
