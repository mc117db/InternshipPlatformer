using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
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
    private CapsuleCollider capsuleCol;
    [SerializeField]
    private float m_wallCollisionCheckDistance = 0.5f;
    private bool m_wallcolliding;


    // Use this for initialization
    void Start () 
	{
        // Cache the references for future access, and also because GetComponent is an expensive operation
        m_rigidbody = gameObject.GetComponent<Rigidbody>();
        m_distToGround = gameObject.GetComponent<Collider>().bounds.extents.y;
        capsuleCol = gameObject.GetComponent<CapsuleCollider>();
    }
	void Update()
    {
        // Check whether the player is grounded to the floor
        // TODO A sphere cast might be necessary to account for convex foot shape of the player.
        m_grounded = Physics.Raycast(transform.position, -Vector3.up, m_distToGround+0.5f);
        /*
        Ray groundCheckRay = new Ray();
        groundCheckRay.direction = Vector3.down;
        groundCheckRay.origin = transform.position - new Vector3(0,m_distToGround+capsuleCol.radius,0);
        m_grounded = Physics.SphereCast(groundCheckRay, capsuleCol.radius);
        */
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
            // If colliding with a wall, don't add any force
            m_rigidbody.AddForce(new Vector3(!m_wallcolliding?(m_grounded ? moveX : moveX * AirControlDegree):0, 0, 0), ForceMode.Impulse);

            // Jump when player is grounded!
            if (Input.GetButtonDown("Jump") && m_grounded)
            {
                m_rigidbody.velocity = new Vector3(m_rigidbody.velocity.x, 0, 0);
                m_rigidbody.AddForce(new Vector3(0, m_jumpHeight, 0),ForceMode.VelocityChange);
            }

            // If absolute horizontal velocity of player is greater than speed, set the x velocity magnitude to speed
            if (Mathf.Abs(m_rigidbody.velocity.x) > Speed)
            {
                m_rigidbody.velocity = new Vector3(Mathf.Sign(m_rigidbody.velocity.x) * Speed, m_rigidbody.velocity.y, 0);
            }

            // Cast a "capsule" onto the horizontal direction the player is moving towards, if it hits something. Set the horizontal velocity to zero
            // We need to do this to prevent player from getting stuck on the wall when he is pressing movement keys against it.
            if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
            {
                RaycastHit hit;
                Vector3 p1 = transform.position + capsuleCol.center + Vector3.up * -capsuleCol.height*0.2f; // Get the bottom of the capsule collider, where the bottom "dome" starts
                Vector2 p2 = p1 + Vector3.up * capsuleCol.height * 0.2f; // Get the top of the capsule collider, where the top "dome" starts
                if (Physics.CapsuleCast(p1, p2, capsuleCol.radius, Vector3.right * Mathf.Sign(Input.GetAxis("Horizontal")), out hit, m_wallCollisionCheckDistance))
                {
                    // I hit something!
                    m_rigidbody.velocity = new Vector3(0, m_rigidbody.velocity.y, 0); // Set the horizontal velocity to zero, will keeping the vertical velocity.
                    m_wallcolliding = true;
                }
                else
                {
                    m_wallcolliding = false;
                }
            }
            else
            {
                m_wallcolliding = false;
            }
        }


       

	}
	






}
