using UnityEngine;
using System.Collections;

public class ComponentCooldown : MonoBehaviour
{

	public float m_cooldownTimer = 0.5f;
	private float m_timeStamp;
	// Use this for initialization
	void Start ()
	{
		m_timeStamp = Time.time + m_cooldownTimer;
	}

	// Update is called once per frame
	void Update ()
	{
		if (m_timeStamp <= Time.time)
		{
			m_timeStamp = Time.time + m_cooldownTimer;

			//TODO Call function
		}
	}
}
