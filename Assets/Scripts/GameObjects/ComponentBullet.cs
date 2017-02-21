using UnityEngine;
using System.Collections;

public class ComponentBullet : MonoBehaviour
{
	[SerializeField]
	protected int m_dmg = 1;


	void Start()
	{

	}
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.name == "platform")
		{
			ComponentHealth a = collider.gameObject.GetComponent<ComponentHealth>();
			if (a != null)
				a.Modify(-m_dmg);

			Func();

		}

	}
	
	public void Func()
	{
		Destroy (gameObject);
	}
}
