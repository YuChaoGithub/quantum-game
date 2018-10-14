using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour 
{
	public GameObject healthBar;
	public GameObject sprite;
	public GameObject bottle;
	public float health;
	public float contactDamage;

	private float fullHealth;
	protected float FullHealth
	{
		get
		{
			return fullHealth;
		}
	}

	protected virtual void Start()
	{
		fullHealth = health;
	}

	protected virtual void OnTriggerEnter2D(Collider2D col)
	{
		//bump into player
		if (col.gameObject.tag == "Player")
		{
			col.gameObject.GetComponent<Player>().Hurt(contactDamage);
		}
		else if (col.gameObject.tag == "Float Player")
		{
			col.gameObject.GetComponent<FloatPlayer>().Hurt(contactDamage);
		}
	}

	public virtual void TakeDamage(int damage)
	{
		if (health - damage <= 0)
		{
			Die();
		}
		else
		{
			health -= damage;
			healthBar.GetComponent<HealthBar>().MinusValue(damage);
		}
	}

	protected virtual void Die()
	{
		health = 0f;
		healthBar.GetComponent<HealthBar>().SetValue(0f);
		bottle.SetActive(true);
	}
}
