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
        // Dot expression checks whether the angle of contact between the colliders is upwards to world space
        // Value of 1 means they are the same direction, -1 opp direction, 0 is perpendicular

        if (col.gameObject.layer != 8 && Vector3.Dot(Vector3.down, col.contacts[0].normal) > 0.3f)
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
