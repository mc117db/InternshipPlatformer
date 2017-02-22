using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class ComponentMovement : MonoBehaviour 
{

	//TODO movement

	public float Speed = 0.1f;
    [Range(0.1f, 1f)]
    public float AirControlDegree = 0.5f;

    Rigidbody m_rigidbody;
	[SerializeField]
	protected float m_jumpHeight = 0;
    private bool m_grounded;
    private float m_distToGround;


    // Use this for initialization
    void Start () 
	{
        // Cache the rigidbody for easy access, and also because GetComponent is an expensive operation
        m_rigidbody = gameObject.GetComponent<Rigidbody>();
        m_distToGround = gameObject.GetComponent<Collider>().bounds.extents.y;
    }
	void Update()
    {
        m_grounded = Physics.Raycast(transform.position, -Vector3.up, m_distToGround+0.5f);
    }
	// Update is called once per frame
	void FixedUpdate () 
	{
        // Calculate the force needed to apply on the rigidbody on the X axes
        // A,D buttons will give -0.1 and 0.1 respectively, no input will return 0;
        // On joysticks, will give float values that range from -0.1 (left) to 0.1 (right)

        float moveX = Input.GetAxis("Horizontal") * Speed;
        // Apply force to the rigidbody
        if (m_rigidbody)
        {
                // This will add force every frame, if player not grounded, horizontal force should be lesser
                m_rigidbody.AddForce(new Vector3(m_grounded ? moveX : moveX * AirControlDegree, 0, 0), ForceMode.Impulse);
         
            if (Input.GetButtonDown("Jump") && m_grounded)
            {
                m_rigidbody.AddForce(new Vector3(0, m_jumpHeight, 0),ForceMode.VelocityChange);
            }

            // If absolute horizontal velocity of player is greater than speed, set the x velocity magnitude to speed
            if (Mathf.Abs(m_rigidbody.velocity.x) > Speed)
            {
                m_rigidbody.velocity = new Vector3(Mathf.Sign(m_rigidbody.velocity.x) * Speed, m_rigidbody.velocity.y, 0);
            }
        }


       

	}
	






}
