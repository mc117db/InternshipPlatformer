using UnityEngine;
using System.Collections;
using System;

public class ComponentHealth : MonoBehaviour,IRecyle
{
	[SerializeField]
	protected int m_MaxHP = 10;
	
	protected int m_CurrHP;
	
	// Getter
	public int CurrHP {
        get	{ return m_CurrHP; }
    }
	public int MaxHP {
        get	{ return m_MaxHP; }
    }
	public float FractionHP {
        get { return (float)(m_CurrHP)/(float)(m_MaxHP); }
    }
    public void Restart()
    {
        m_CurrHP = m_MaxHP;
        Modify(0);
    }

    public void Shutdown()
    {
        // NO OP
    }

    void Start()
	{
        Restart();
	}

	public void Damage(int amount)
    {
        //Debug.Log(gameObject.name + " took " + amount + " damage");
        Modify(-amount);
    }

    public void Heal(int amount)
    {
        //Debug.Log(gameObject.name + " restored " + amount + " health");
        Modify(amount);
    }

	private void Modify(int amount)
	{
		m_CurrHP += amount;
		
		if (m_CurrHP > m_MaxHP)
		{
			m_CurrHP = m_MaxHP;
		}
		else if (m_CurrHP <= 0)
		{
			Die();
		}
	}
	
	private void Set(int amount)
	{
		m_CurrHP = amount;
		Modify (0); // run thru the checks in Modify()
	}

    public void HealToMax()
    {
        Set(MaxHP);
    }
		
	public void Die()
	{
        GameObjectUtil.Destroy(gameObject);
		//Destroy(this.gameObject);
	}

    
}
