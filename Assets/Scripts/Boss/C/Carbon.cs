using UnityEngine;
using System.Collections;

public class Carbon : Boss
{
	private Animator animator;

	//for shooting
	public GameObject leftKnifePos;
	public GameObject rightKnifePos;
	public GameObject knifePrefab;
	private const float ShootTimeLowerBound = 0.5f;
	private const float ShootTimeUpperBound = 1.5f;
	private const float IndicationTime = 0.3f;

	public GameObject beingHitAudio;
	public GameObject throwKnifeAudio;

	//for dying
	public GameObject dieShreds;
	private const float DieAnimationTime = 1f;

	//for movement
	private Vector3 leftBound = new Vector3(222.5f, 21.5f, 0f);
	private Vector3 rightBound = new Vector3(243f, 21.5f, 0f);
	private const float TimeUpperBound = 3f;
	private const float TimeLowerBound = 2f;
	private const float StopTimeLowerBound = .5f;
	private const float StopTimeUpperBound = 3f;

	protected override void Start()
	{
		base.Start();

		animator = base.sprite.GetComponent<Animator>();

		StartCoroutine(Movement());
		StartCoroutine(Shooting());
	}

	protected override void Die()
	{
		base.bottle.transform.position = transform.position;
		base.Die();

		animator.SetTrigger("Die");
		Invoke("Remove", DieAnimationTime);
	}

	void Remove()
	{
		Instantiate(dieShreds, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}

	public override void TakeDamage(int damage)
	{
		if (health == 0) return;

		base.TakeDamage(damage);
		Instantiate(beingHitAudio, transform.position, Quaternion.identity);
	}

	IEnumerator Movement()
	{
		while (base.health > 0f)
		{
			//randomize movement and time
			Vector3 objective = new Vector3(Random.Range(leftBound.x, rightBound.x), leftBound.y, 0f);
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

	IEnumerator Shooting()
	{
		while (health > 0f)
		{
			yield return new WaitForSeconds(Random.Range(ShootTimeLowerBound, ShootTimeUpperBound));

			//indicate
			animator.SetBool("Shooting", true);
			yield return new WaitForSeconds(IndicationTime);

			//shoot
			animator.SetBool("Shooting", false);
			Instantiate(throwKnifeAudio, transform.position, Quaternion.identity);
			Instantiate(knifePrefab, leftKnifePos.transform.position, Quaternion.identity);
			Instantiate(knifePrefab, rightKnifePos.transform.position, Quaternion.identity);
		}
	}
}
