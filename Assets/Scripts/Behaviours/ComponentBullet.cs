using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]

public class ComponentBullet : MonoBehaviour ,IRecyle
{
	private int m_dmg = 1;
    private Vector3 direction; // This should be a normalised vector
    private float m_speed = 1;
    private Rigidbody m_rigidbody;

    public void SetBulletValue(Vector3 dir)
    {
        direction = dir;
    }
    public void SetBulletValue(Vector3 dir,float spd, int dmg)
    {
        direction = dir;
        m_speed = spd;
        m_dmg = dmg;
    }

	void Start()
	{
        Restart();
    }
	// Update is called once per frame
	void Update ()
	{
	    if (!m_rigidbody)
        {
            return;
        }
        m_rigidbody.velocity = direction * m_speed;
	}

	void OnCollisionEnter(Collision col)
	{
        ComponentHealth a;
        if (col.gameObject.GetComponent<ComponentHealth>() != null)
        {
            a = col.gameObject.GetComponent<ComponentHealth>();
            a.Damage(m_dmg);
        }
		DestroyGameObject();
	}
	
	public void DestroyGameObject()
	{
        GameObjectUtil.Destroy(gameObject);
	}

    // GameObject Pooling functions
    public void Restart()
    {
       // RecycleGameObject will call upon this function on intilisation from the Pool
       if (!m_rigidbody)
        {
            //Cache the rigidbody
            m_rigidbody = gameObject.GetComponent<Rigidbody>();
        }
    }

    public void Shutdown()
    {
        direction = Vector3.zero;
        m_speed = 0;
    }
}
