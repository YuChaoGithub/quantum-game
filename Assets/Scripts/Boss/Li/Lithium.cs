using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Lithium : Boss
{
	public GameObject[] shooters;

	public GameObject beingHitAudio;
	public GameObject spinAudio;

	public float spinAudioConstant;

	private float shootInterval = 3f;
	private Animator animator;
	private bool beingHit = false;
	private float prevAngle;

	private const float ShootIntervalSpeedingConst = 0.2f;
	private const float ShootIntervalRandomRange = 0.5f;
	private const float ShootIntervalCap = 1f;
	private const float SpinConstant = 0.06f;
	private const float ShootAnimationTime = 1f;
	private const float DieAnimationTime = 0.82f;
	private const float BeingHitAnimationTime = 0.2f;


	protected override void Start()
	{
		base.Start();

		animator = base.sprite.GetComponent<Animator>();
		StartCoroutine(Cycle());
	}

	void Update()
	{
		//spin
		//print(""+prevAngle + " " + base.sprite.transform.eulerAngles);

		if (base.sprite.transform.eulerAngles.z < 60f && prevAngle > 300f || base.sprite.transform.eulerAngles.z > 180f && prevAngle < 180f)			
			Instantiate(spinAudio, transform.position, Quaternion.identity);
		
		prevAngle = base.sprite.transform.eulerAngles.z;

		base.sprite.transform.Rotate(0f, 0f, SpinConstant * (FullHealth - health));
	}

	protected override void Die()
	{
		base.Die();

		StopAllCoroutines();
		animator.SetTrigger("Die");
		Invoke("Remove", DieAnimationTime);
	}

	void Remove()
	{
		Destroy(gameObject);
	}

	public override void TakeDamage(int damage)
	{
		if (beingHit) return;

		base.TakeDamage(damage);

		beingHit = true;
		animator.SetBool("Hit", true);
		Instantiate(beingHitAudio, transform.position, Quaternion.identity);
		Invoke("ResetBeingHit", BeingHitAnimationTime);
	}

	void ResetBeingHit()
	{
		animator.SetBool("Hit", false);
		beingHit = false;
	}

	IEnumerator Cycle()
	{
		while (true)
		{
			//normal state
			yield return new WaitForSeconds(Random.Range(shootInterval-ShootIntervalRandomRange, shootInterval-ShootIntervalRandomRange));

			//Charge Animation
			animator.SetTrigger("Charge");
			yield return new WaitForSeconds(ShootAnimationTime);

			//Shoot lightning (HP 0~100: 3 lightnings, HP 101~200: 2 lightnings, HP 201~300: 3 lightnings)
			int bulletNumbers = Mathf.FloorToInt((base.FullHealth - base.health) / 100f);

			List<int> selectedShooters = new List<int>();
			for (int i = 0; i <= bulletNumbers; i++)
			{
				int randomShooter;

				//make sure numbers of different shooters are selected
				while (true)
				{
					bool distinct = true;
					randomShooter = Random.Range(0, shooters.Length);
					foreach (int num in selectedShooters)
					{
						if (num == randomShooter) distinct = false;
					}
					if (distinct) break;
				}

				selectedShooters.Add(randomShooter);
			}
			//print(selectedShooters.Count);
			foreach (int i in selectedShooters)
			{
				shooters[i].GetComponent<LightningShooter>().Shoot();
			}
				
		}
	}
}
