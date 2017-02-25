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
    private Rigidbody m_rigidbody;

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
        m_rigidbody = GetComponent<Rigidbody>();
    }
    
    public void FixedUpdate()
    {
        if (transform.position.x > StartPos.x + TravelDistance || transform.position.x < StartPos.x)
        {
            // Invert the direction vector
            travelDirection = -travelDirection;
        }

        if (m_rigidbody)
        {
        m_rigidbody.MovePosition(transform.position + travelDirection * movespeed * Time.deltaTime);
        }
     
    }
}
