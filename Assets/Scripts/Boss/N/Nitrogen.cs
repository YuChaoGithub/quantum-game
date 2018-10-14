using UnityEngine;
using System.Collections;

public class Nitrogen : Boss
{
	private Animator animator;

	//for shooting
	public GameObject shootPosition;
	public GameObject yellowChip;
	public GameObject redChip;

	//audio
	public GameObject dieAudio;
	public GameObject openAudio;
	public GameObject beingHitAudio;

	private Vector2 ForceForChip1 = new Vector2(50f, 50f);
	private Vector2 ForceForChip2 = new Vector2(100f, 100f);
	private Vector2 ForceForChip3 = new Vector2(200f, 100f);
	private Vector2 ForceForChip4 = new Vector2(-50f, 50f);
	private Vector2 ForceForChip5 = new Vector2(-100f, 100f);
	private Vector2 ForceForChip6 = new Vector2(-200f, 100f);
	private int MinShootingWaves = 1;
	private int MaxShootingWaves = 5;
	private float IntervalBetweenWaves = 0.5f;

	//for movement
	private Vector3 upperMovementBound = new Vector3(230f, 23.5f, 0f);
	private Vector3 lowerMovementBound = new Vector3(230f, 45f, 0f);
	private const float MovementSpeed = 3f;

	//for animation
	private const float DieAnimationTime = 0.5f;
	private const float BossOpenCloseAnimationTime = 0.5f;

	protected override void Start()
	{
		base.Start();

		animator = base.sprite.GetComponent<Animator>();

		StartCoroutine(BossCycle());
	}

	protected override void Die()
	{
		base.Die();

		base.bottle.transform.position = shootPosition.transform.position;
		animator.SetTrigger("Die");
		Instantiate(dieAudio, transform.position, Quaternion.identity);
		Invoke("Remove", DieAnimationTime);
	}

	void Remove()
	{
		Destroy(gameObject);
	}

	public override void TakeDamage(int damage)
	{
		if (health == 0) return;

		base.TakeDamage(damage);
		Instantiate(beingHitAudio, transform.position, Quaternion.identity);
	}
		

	IEnumerator BossCycle()
	{
		while (base.health > 0f)
		{
			//move to a spot
			float newY = Random.Range(lowerMovementBound.y, upperMovementBound.y);
			Vector3 newPos = new Vector3(upperMovementBound.x, newY, 0f);
			float time = Mathf.Abs(newY - transform.position.y) / MovementSpeed;
			StartCoroutine(MoveToObjective(transform.position, newPos, time));
			yield return new WaitForSeconds(time);
			StopCoroutine("MoveToObjective");

			//open animation
			animator.SetTrigger("Open");
			Instantiate(openAudio, transform.position, Quaternion.identity);
			yield return new WaitForSeconds(BossOpenCloseAnimationTime);

			//throw chips
			int waves = Random.Range(MinShootingWaves, MaxShootingWaves);
			for (int i = 0; i < waves; i++)
			{
				GameObject chip1 = (Instantiate((Random.value > 0.5f ? redChip : yellowChip), shootPosition.transform.position, Quaternion.identity) as GameObject);
				GameObject chip2 = (Instantiate((Random.value > 0.5f ? redChip : yellowChip), shootPosition.transform.position, Quaternion.identity) as GameObject);
				GameObject chip3 = (Instantiate((Random.value > 0.5f ? redChip : yellowChip), shootPosition.transform.position, Quaternion.identity) as GameObject);
				GameObject chip4 = (Instantiate((Random.value > 0.5f ? redChip : yellowChip), shootPosition.transform.position, Quaternion.identity) as GameObject);
				GameObject chip5 = (Instantiate((Random.value > 0.5f ? redChip : yellowChip), shootPosition.transform.position, Quaternion.identity) as GameObject);
				GameObject chip6 = (Instantiate((Random.value > 0.5f ? redChip : yellowChip), shootPosition.transform.position, Quaternion.identity) as GameObject);

				chip1.GetComponent<Rigidbody2D>().AddForce(ForceForChip1);
				chip2.GetComponent<Rigidbody2D>().AddForce(ForceForChip2);
				chip3.GetComponent<Rigidbody2D>().AddForce(ForceForChip3);
				chip4.GetComponent<Rigidbody2D>().AddForce(ForceForChip4);
				chip5.GetComponent<Rigidbody2D>().AddForce(ForceForChip5);
				chip6.GetComponent<Rigidbody2D>().AddForce(ForceForChip6);
				yield return new WaitForSeconds(IntervalBetweenWaves);
			}

			//close animation
			animator.SetTrigger("Close");
			yield return new WaitForSeconds(BossOpenCloseAnimationTime);
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
