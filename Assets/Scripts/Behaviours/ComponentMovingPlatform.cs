using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ComponentMovingPlatform : MonoBehaviour 
{

    //TODO Moving Platform

    public float movespeed; //how quickly the platform moves
    private Vector3 travelDirection;      //direction of travel
    public float TravelDistance;    //how far to move before reverse
    Vector3 StartPos;
    private List<Rigidbody> contacts = new List<Rigidbody>();

    public void Start()
    {
        if (movespeed <= 0)
        {
            movespeed = 2f;
        }
        if (TravelDistance <= 0)
        {
            TravelDistance = 2f;
        }
        if (travelDirection == Vector3.zero)
        {
            travelDirection = Vector3.right;
        }
        StartPos = transform.position;
    }
  
    public void Update()
    {
        if (transform.position.x > StartPos.x + TravelDistance)
        {
            travelDirection = -travelDirection;
        }

        else if (transform.position.x < StartPos.x)
        {
            travelDirection = -travelDirection;
        }

        transform.Translate(travelDirection * movespeed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision col)
    {
        // Add the GameObject collided with to the list.
        // 8 is reserved for bullets
        if (col.gameObject.layer != 8)
        {
            contacts.Add(col.gameObject.GetComponent<Rigidbody>());
        }
    }

    void OnCollisionExit(Collision col)
    {
        // Remove the GameObject collided with from the list.
        contacts.Remove(col.gameObject.GetComponent<Rigidbody>());
    }

    public void FixedUpdate()
    {
        if (contacts.Count > 0)
        {

            foreach (Rigidbody contact in contacts)
            {
                //contact.AddForce(travelDirection * movespeed,ForceMode.Impulse);
                //
                if (contact != null)
                {
                    contact.MovePosition(contact.position + travelDirection * movespeed * Time.deltaTime);
                }
                else
                {
                    contacts.Remove(contact);
                }
                
            }
        }
    }
}
