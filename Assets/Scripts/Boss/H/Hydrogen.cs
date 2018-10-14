using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Hydrogen : Boss
{
	public GameObject emitPosition;
	public GameObject bullet;
	public GameObject explodingObjects;

	//audio
	public GameObject beingHitAudio;
	public GameObject expandAudio;
	public GameObject shootAudio;
	
	//for animation
	private Animator animator;
	//emit
	private const float EmitTime = 1f;
	private const float InitialColliderRadius = 0.98f;
	private const float EmitColliderRadius = 1.5f;
	private const float ColliderLerpingTime1 = 0.5f;
	private const float ColliderLerpingTime2 = 0.05f;
	//explode
	private const float ExplodeExpandTime1 = 0.25f;
	private const float ExplodeWaitTime = 1f;
	private const float ExplodeExpandTime2 = 0.167f;
	private const float ExplodeEndTime = 0.5f;
	private const float ExplodeExpandRadius1 = 1.5f;
	private const float ExplodeExpandRadius2 = 3.3f;
	private const float RemoveExplosionEffectorTime = 0.2f;

	//control electron bullet emit
	private bool canEmit = true;
	private const float MinEmitInterval = 3f;
	private const float EmitChancePerSec = 0.7f;

	//for tinting when being hit by player
	private bool tinting = false;
	private const float TintDuration = 0.1f;

	//for movement
	private Vector3 upperBound = new Vector3(240f, 15f, 0f);
	private Vector3 lowerBound = new Vector3(235f, 30f, 0f);
	private Vector3 explodePosition = new Vector3(225f, 22f, 0f);
	private const float TimeUpperBound = 3f;
	private const float TimeLowerBound = 1f;
	private const float StopTimeLowerBound = .5f;
	private const float StopTimeUpperBound = 3f;
	private const float MoveToExplodeTime = 1f;

	protected override void Start()
	{
		base.Start();

		animator = base.sprite.GetComponent<Animator>();

		InvokeRepeating("CheckIfEmit", 1f, 1f);

		StartCoroutine(Movement());
	}

	void CheckIfEmit()
	{
		if (canEmit && Random.value < EmitChancePerSec)
		{
			canEmit = false;
			
			animator.SetTrigger("Emit");
			Instantiate(expandAudio, transform.position, Quaternion.identity);
			StartCoroutine(ColliderLerp(InitialColliderRadius, EmitColliderRadius, ColliderLerpingTime1));

			Invoke("Emit", EmitTime);
		}
	}

	void Emit()
	{
		Instantiate(bullet, emitPosition.transform.position, Quaternion.identity);
		Instantiate(shootAudio, transform.position, Quaternion.identity);
		StopCoroutine("ColliderLerp");

		//animate back the collider
		StartCoroutine(ColliderLerp(EmitColliderRadius, InitialColliderRadius, ColliderLerpingTime2));

		Invoke("ResetCooldown", MinEmitInterval);
	}

	void ResetCooldown()
	{
		canEmit = true;
	}

	public override void TakeDamage(int damage)
	{
		if (health == 0) return;

		base.TakeDamage(damage);

		if (!tinting) Tint();
	}

	void Tint()
	{
		tinting = true;
		base.sprite.GetComponent<SpriteRenderer>().color = Color.red;
		Instantiate(beingHitAudio, transform.position, Quaternion.identity);

		Invoke("RecoverTint", TintDuration);
	}

	void RecoverTint()
	{
		tinting = false;
		base.sprite.GetComponent<SpriteRenderer>().color = Color.white;
	}

	IEnumerator ColliderLerp(float startRadius, float endRadius, float time)
	{
		float i = 0f;
		float scale = 1f/time;

		while (i < 1f)
		{
			i += Time.deltaTime * scale;
			transform.GetComponent<CircleCollider2D>().radius = Mathf.Lerp(startRadius, endRadius, i);
			yield return null;
		}
	}

	protected override void Die()
	{
		base.Die();

		canEmit = false;
		animator.SetTrigger("Die");

		StartCoroutine(Explode());
	}

	IEnumerator Explode()
	{
		//move to position
		StartCoroutine(MoveToObjective(transform.position, explodePosition, MoveToExplodeTime));

		//first expansion
		StartCoroutine(ColliderLerp(InitialColliderRadius, ExplodeExpandRadius1, ExplodeExpandTime1));
		yield return new WaitForSeconds(ExplodeExpandTime1 + ExplodeWaitTime);

		//second expansion
		StartCoroutine(ColliderLerp(ExplodeExpandRadius1, ExplodeExpandRadius2, ExplodeExpandTime2));
		yield return new WaitForSeconds(ExplodeExpandTime2 + ExplodeEndTime);

		Destroy(gameObject);
		GameObject explosionDebris = (Instantiate(explodingObjects, transform.position, Quaternion.identity)) as GameObject;
		yield return new WaitForSeconds(RemoveExplosionEffectorTime);

		explosionDebris.GetComponent<PointEffector2D>().enabled = false;
	}

	IEnumerator Movement()
	{
		while (base.health > 0f)
		{
			//randomize movement and time
			Vector3 objective = new Vector3(upperBound.x, Random.Range(lowerBound.y, upperBound.y), 0f);
			float time = Random.Range(TimeLowerBound, TimeUpperBound);
			float stopTime = Random.Range(StopTimeLowerBound, StopTimeUpperBound);

			//start moving
			StartCoroutine(MoveToObjective(transform.position, objective, time));

			yield return new WaitForSeconds(time + stopTime);
			StopCoroutine("MoveToObjective");
		}
	}
	
	IEnumerator MoveToObjective(Vector3 init, Vector3 final, float time)
	{
		float i = 0f;
		while (i <= 1)
		{
			i += Time.deltaTime/time;
			Vector3 newPos = Vector3.Lerp(init, final, i);
			transform.position = newPos;
			yield return null;
		}
	}
}
