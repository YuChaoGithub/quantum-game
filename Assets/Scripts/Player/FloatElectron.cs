using UnityEngine;
using System.Collections;

public class FloatElectron : FloatPlayer 
{
	public GameObject knife;
	public GameObject[] knifePositions;

	public GameObject bomb;
	public GameObject bombPosition;

	public GameObject proton;

	public GameObject electronToProtonAudio;

	private bool canShootKnife = true;
	private bool canShootBomb = true;

	private const float KnifeCooldown = 1f;
	private const float BombCooldown = 3f;

	protected override void EndSwitch()
	{
		base.EndSwitch();
		proton.GetComponent<FloatPlayer>().Positioning();
		proton.SetActive(true);
		this.gameObject.SetActive(false);
		avatar.SetActive(false);
		base.coolDownIndicator.GetComponent<CooldownIndicator>().SwitchTo(1);
	}

	protected override void FireBullet1()
	{
		if (canShootKnife)
		{
			canShootKnife = false;
			Invoke("ResumeCanShootKnife", KnifeCooldown);
			
			Instantiate(knife, knifePositions[0].transform.position, Quaternion.identity);
			Instantiate(knife, knifePositions[1].transform.position, Quaternion.identity);
			
			coolDownIndicator.GetComponent<CooldownIndicator>().electronKnife.GetComponent<CooldownIcon>().StartCountDown();
		}
	}
	
	void ResumeCanShootKnife()
	{
		canShootKnife = true;
	}
	
	protected override void FireBullet2()
	{
		if (canShootBomb)
		{
			canShootBomb = false;
			Invoke("ResumeCanShootBomb", BombCooldown);
			
			Instantiate(bomb, bombPosition.transform.position, Quaternion.identity);
			
			coolDownIndicator.GetComponent<CooldownIndicator>().electronBomb.GetComponent<CooldownIcon>().StartCountDown();
		}
	}
	
	protected override void Switch()
	{
		if (proton.GetComponent<FloatPlayer>().IsAlive())
		{
			base.Switch();
			Instantiate (electronToProtonAudio, transform.position, Quaternion.identity);
			base.spriteAnimator.SetTrigger("Transform");
		}
	}
	
	void ResumeCanShootBomb()
	{
		canShootBomb = true;
	}
	
	protected override void Die()
	{
		base.Die();
		
		if (!proton.GetComponent<FloatPlayer>().IsAlive())
		{
			base.gameoverScene.SetActive(true);
			gameObject.SetActive(false);
		}
		else
		{
			Switch();
		}
	}
}
