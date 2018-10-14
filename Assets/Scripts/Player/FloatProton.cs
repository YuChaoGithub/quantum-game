using UnityEngine;
using System.Collections;

public class FloatProton : FloatPlayer 
{
	//bullet prefabs
	public GameObject wave;
	public GameObject wavePositionRight;
	public GameObject wavePositionLeft;
	
	public GameObject electron;

	public GameObject protonToElectronAudio;
	public GameObject shootAudio;
	public GameObject goldenAudio;
	private bool canShootWave = true;

	private bool hiding = false;
	private bool canHide = true;
	
	private const float WaveCooldown = 1f;
	private const float HideCooldown = 5f;
	
	protected override void EndSwitch()
	{
		base.EndSwitch();

		electron.GetComponent<FloatPlayer>().Positioning();
		electron.SetActive(true);
		this.gameObject.SetActive(false);
		avatar.SetActive(false);
		base.coolDownIndicator.GetComponent<CooldownIndicator>().SwitchTo(0);
	}

	protected override void FireBullet1()
	{
		if (canShootWave && base.CanMove)
		{
			canShootWave = false;
			Instantiate (shootAudio, transform.position, Quaternion.identity);
			if (side == 1)
			{
				GameObject bullet = (Instantiate(wave, wavePositionRight.transform.position, Quaternion.identity)) as GameObject;
				bullet.GetComponent<Bullet>().side = side;
			}
			else
			{
				GameObject bullet = (Instantiate(wave, wavePositionLeft.transform.position, Quaternion.identity)) as GameObject;
				bullet.GetComponent<Bullet>().side = side;
			}
			
			coolDownIndicator.GetComponent<CooldownIndicator>().protonWave.GetComponent<CooldownIcon>().StartCountDown();
			
			Invoke("ResetCanShoot", WaveCooldown);
		}
	}
	
	void ResetCanShoot()
	{
		canShootWave = true;
	}
	
	protected override void FireBullet2()
	{
		if (hiding)
		{
			ResumeHide();
		}
		else if (canHide && !base.isTransforming)
		{
			Hide();
		}
	}
	
	void ResumeHide()
	{
		hiding = false;
		base.CanMove = true;
		Invoke("ResumeCanHide",HideCooldown);
		sprite.GetComponent<Animator>().SetBool("Hide", false);
		GetComponent<CircleCollider2D>().enabled = true;
		GetComponent<Rigidbody2D>().isKinematic = false;
		coolDownIndicator.GetComponent<CooldownIndicator>().protonHide.GetComponent<CooldownIcon>().StartCountDown();
	}
	
	void ResumeCanHide()
	{
		canHide = true;
	}
	
	void Hide()
	{
		canHide = false;
		hiding = true;
		base.CanMove = false;
		sprite.GetComponent<Animator>().SetBool("Hide", true);
		GetComponent<CircleCollider2D>().enabled = false;
		GetComponent<Rigidbody2D>().isKinematic = true;
		Instantiate(goldenAudio, transform.position, Quaternion.identity);
		coolDownIndicator.GetComponent<CooldownIndicator>().protonHide.GetComponent<CooldownIcon>().ToggleSkill();
	}
	
	protected override void Switch()
	{
		if (electron.GetComponent<FloatPlayer>().IsAlive())
		{
			base.Switch();
			Instantiate(protonToElectronAudio, transform.position, Quaternion.identity);
			spriteAnimator.SetTrigger("Transform");
		}
	}
	
	protected override void Die()
	{
		base.Die ();

		if (!electron.GetComponent<FloatPlayer>().IsAlive())
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
