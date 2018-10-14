using UnityEngine;
using System.Collections;

public class ChargedPlayer : Player
{
	public float health;
	public GameObject healthBar;

	private bool canBeHurtByLaser = true;
	private float fullHealth;
	private bool hurt = false;

	public GameObject hurtAudio;
	
	private const float HurtTime = 1f;
	private const float LaserHitInterval = 0.1f;
	private const float TransformTime = 0.5f;
	

	protected override void Start()
	{
		base.Start();

		fullHealth = health;
	}

	public override void Hurt(float damage)
	{
		base.Hurt(damage);

		if (!base.alive)
			return;

		if (health - damage <= 0) {
			health = 0;
			healthBar.GetComponent<HealthBar>().SetValue (0);
			Die ();
		} 
		else 
		{
			Instantiate(hurtAudio, transform.position, Quaternion.identity);
			health -= damage;
			healthBar.GetComponent<HealthBar> ().MinusValue (damage);

			spriteAnimator.SetBool ("Hurt", true);
			Invoke ("ResetHurt", HurtTime);
		}
	}

	void OnCollisionStay2D(Collision2D col)
	{
		//base.OnCollisionStay2D(col);
		
		//hurt by monster
		if (!hurt && (col.gameObject.tag == "Monster" || col.gameObject.tag == "Float Monster"))
		{
			float damage;

			if (col.gameObject.tag == "Monster")
				damage = col.gameObject.GetComponent<Enemy>().damage;
			else
				damage = col.gameObject.GetComponent<FloatEnemy>().damage;
			
			hurt = true;
			
			Hurt (damage);
		}
	}

	void ResetHurt()
	{
		if (!base.alive || base.isTransforming)
			return;
		hurt = false;
		base.spriteAnimator.SetBool("Hurt", false);
	}

	protected virtual void Die()
	{
		base.alive = false;
		Instantiate(dieSound, transform.position, Quaternion.identity);
	}

	public override void HitByLaser()
	{
		if (canBeHurtByLaser)
		{
			canBeHurtByLaser = false;
			Invoke("ResetCanBeHurtByLaser", LaserHitInterval);
			Hurt(5f);
		}
	}

	void ResetCanBeHurtByLaser()
	{
		canBeHurtByLaser = true;
	}

	public override void Recover(float amount)
	{
		if (health + amount <= fullHealth)
		{
			health += amount;
			healthBar.GetComponent<HealthBar>().AddValue(amount);
		}
		else
		{
			health = fullHealth;
			healthBar.GetComponent<HealthBar>().SetValue(fullHealth);
		}
	}

	protected override void Switch()
	{
		base.Switch();

		Invoke("ResetIsTransforming", TransformTime);
	}

	void ResetIsTransforming()
	{
		base.isTransforming = false;
		EndSwitch();
	}
}
