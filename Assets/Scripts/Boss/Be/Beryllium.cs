using UnityEngine;
using System.Collections;

public class Beryllium : Boss
{
	private Vector3 leftMostPosition;
	private Vector3 rightMostPosition;
	private float side = -1f;

	public GameObject beingHitAudio;
	public GameObject dieAudio;

	private const float MinSpinningSpeed = 2f;
	private const float MinSpeed = 1f;
	private const float MaxSpinningSpeed = 6f;
	private const float MaxSpeed = 5f;
	private const float DieAnimationTimer = 1f;

	protected override void Start ()
	{
		base.Start();

		leftMostPosition = new Vector3(215.5f, 24f);
		rightMostPosition = new Vector3(245.5f, 24f);

		StartCoroutine(Movement());
	}

	public override void TakeDamage(int damage)
	{
		if (health == 0) return;

		base.TakeDamage(damage);
		Instantiate(beingHitAudio, transform.position, Quaternion.identity);
	}

	protected override void Die()
	{
		base.Die();

		base.bottle.transform.position = transform.position;
		base.sprite.GetComponent<Animator>().SetTrigger("Die");
		Instantiate(dieAudio, transform.position, Quaternion.identity);
		Invoke("DestroyGameObj", DieAnimationTimer);
	}

	void DestroyGameObj()
	{
		Destroy(gameObject);
	}

	void Update()
	{
		base.sprite.transform.Rotate(0f, 0f, MinSpinningSpeed + (MaxSpinningSpeed - MinSpinningSpeed) * (1f-(base.health / base.FullHealth)) * side);
	}

	IEnumerator Movement()
	{
		while (base.health > 0)
		{
			float time;
			side *= -1;
			//move to left
			time = 10f / (MinSpeed + (MaxSpeed - MinSpeed) * (1f-(base.health / base.FullHealth)));
			StartCoroutine(MoveToObjective(transform.position, leftMostPosition, time));
			yield return new WaitForSeconds(time);
			StopCoroutine("MoveToObjective");

			side *= -1;
			//move to right
			time = 10f / (MinSpeed + (MaxSpeed - MinSpeed) * (1f-(base.health / base.FullHealth)));
			StartCoroutine(MoveToObjective(transform.position, rightMostPosition, time));
			yield return new WaitForSeconds(time);
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
