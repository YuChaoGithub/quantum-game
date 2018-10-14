using UnityEngine;
using System.Collections;

public class Neutron : Player 
{
	public GameObject bullet;
	public GameObject[] firePositions;

	public GameObject healthBar;

	public GameObject proton;
	
	private int bulletCount = 0;

	private const int FireTimes = 10;
	private const float Duration = 8f;
	private const float BulletInterval = 0.1f;
	
	protected override void OnEnable()
	{
		base.OnEnable();

		healthBar.SetActive(true);

		//auto destroy after duration
		Invoke("End", Duration);

		bulletCount = 0;

		StartCoroutine(HealthBarLerp());
	}

	IEnumerator HealthBarLerp()
	{
		float i = 0f;
		
		while (i < 1f)
		{
			i += Time.deltaTime / Duration;
			healthBar.GetComponent<HealthBar>().SetValue(Mathf.Lerp(Duration, 0f, i));
			yield return null;
		}
	}

	protected override void EndSwitch()
	{
		base.EndSwitch();
		proton.GetComponent<Player>().Positioning();
		proton.SetActive(true);
		this.gameObject.SetActive(false);
		avatar.SetActive(false);
		base.coolDownIndicator.GetComponent<CooldownIndicator>().SwitchTo(1);
	}

	protected override void FireBullet1()
	{
		End();
	}

	void OnCollisionStay2D(Collision2D col)
	{
		//base.OnCollisionStay2D(col);

		if (col.gameObject.tag == "Monster")
		{
			col.gameObject.GetComponent<Enemy>().BeingHit();
		}
	}

	protected override void FireBullet2()
	{
		End();
	}

	void End()
	{
		CancelInvoke("End");
		InvokeRepeating("ShootBullet", 0f, BulletInterval);
		Switch();

		//empty health bar
		healthBar.GetComponent<HealthBar>().SetValue(0f);

		coolDownIndicator.GetComponent<CooldownIndicator>().neutronBullet.GetComponent<CooldownIcon>().StartCountDown();
	}

	void ShootBullet()
	{
		if (bulletCount == FireTimes)
		{
			CancelInvoke("ShootBullet");
		}
		else
		{
			bulletCount++;

			//right side
			GameObject bullet1 = (Instantiate(bullet, firePositions[0].transform.position, Quaternion.identity)) as GameObject;
			bullet1.GetComponent<Bullet>().side = base.side;
			
			//left side
			GameObject bullet2 = (Instantiate(bullet, firePositions[1].transform.position, Quaternion.identity)) as GameObject;
			bullet2.GetComponent<Bullet>().side = -base.side;
		}
	}

	protected override void Switch()
	{
		base.Switch();

		base.spriteAnimator.SetTrigger("Transform2");

		Invoke("ResetIsTransforming", FireTimes * BulletInterval);

		StopAllCoroutines();
	}

	void ResetIsTransforming()
	{
		isTransforming = false;
		EndSwitch();
	}
}
