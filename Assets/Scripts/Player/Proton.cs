using UnityEngine;
using System.Collections;

public class Proton : ChargedPlayer
{
	//bullet prefabs
	public GameObject wave;
	public GameObject wavePositionRight;
	public GameObject wavePositionLeft;

	public GameObject electron;
	public GameObject neutron;

	public GameObject goldenAudio;
	public GameObject shootAudio;
	public GameObject protonToElectronAudio;
	public GameObject protonToNeutronAudio;
	
	private bool canShootWave = true;
	private int switchType = 1;
	private bool hiding = false;
	private bool canHide = true;

	private const float WaveCooldown = 1f;
	private const float HideCooldown = 5f;

	protected override void EndSwitch()
	{
		base.EndSwitch();
		switch (switchType)
		{
		case 1:
			electron.GetComponent<Player>().Positioning();
			electron.SetActive(true);
			this.gameObject.SetActive(false);
			avatar.SetActive(false);
			base.coolDownIndicator.GetComponent<CooldownIndicator>().SwitchTo(0);
			break;
		case 2:
			neutron.GetComponent<Player>().Positioning();
			neutron.SetActive(true);
			this.gameObject.SetActive(false);
			avatar.SetActive(false);
			switchType = 1;
			base.coolDownIndicator.GetComponent<CooldownIndicator>().SwitchTo(2);
			break;
		}
	}

	public void SwitchToNeutron()
	{
		switchType = 2;
		Switch();

	}

	protected override void FireBullet1()
	{
		if (canShootWave && base.CanMove)
		{
			canShootWave = false;

			Instantiate(shootAudio, transform.position, Quaternion.identity);
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
		switch (switchType)
		{
			case 1: 
				if (electron.GetComponent<Player>().IsAlive())
				{
					base.Switch();
					Instantiate(protonToElectronAudio, transform.position, Quaternion.identity);
					spriteAnimator.SetTrigger("Transform");
				}
				break;

			case 2: 
				base.Switch();	
				Instantiate(protonToNeutronAudio, transform.position, Quaternion.identity);
				spriteAnimator.SetTrigger("Transform2");
				break;
		}
	}

	protected override void Die()
	{
		base.Die ();

		if (!electron.GetComponent<Player>().IsAlive())
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
