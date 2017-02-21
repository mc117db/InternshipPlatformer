using UnityEngine;
using System.Collections;

public class ComponentShoot : MonoBehaviour
{
	public GameObject m_PrefabBullet;

	[SerializeField]
	protected float m_Speed = 20.0f;
	
	// Update is called once per frame
	void Update ()
	{
		// FIRE!
		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			// TODO: Implement shoot
		}

				
	}

}
