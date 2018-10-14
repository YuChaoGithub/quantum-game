using UnityEngine;
using System.Collections;

public class Scandium : Boss
{
	public GameObject scBallPrefab;
	public GameObject beingHitAudio;
	private const float RotationSpeed = 50f;
	private bool tinting = false;
	private const float TintDuration = 0.1f;

	protected override void Die ()
	{
		base.Die();

		Instantiate(scBallPrefab, base.sprite.transform.position, Quaternion.identity);
		Instantiate(scBallPrefab, base.sprite.transform.position, Quaternion.identity);
		Instantiate(scBallPrefab, base.sprite.transform.position, Quaternion.identity);

		Destroy(gameObject);
	}

	void Update()
	{
		transform.Rotate(new Vector3(0f, 0f, RotationSpeed * Time.deltaTime));
		base.sprite.transform.Rotate(new Vector3(0f, 0f, RotationSpeed * Time.deltaTime));
	}

	public override void TakeDamage(int damage)
	{
		if (health == 0) return;

		base.TakeDamage(damage);

		Instantiate(beingHitAudio, transform.position, Quaternion.identity);
		if (!tinting) Tint();
	}

	void Tint()
	{
		tinting = true;
		base.sprite.GetComponent<SpriteRenderer>().color = Color.red;

		Invoke("RecoverTint", TintDuration);
	}

	void RecoverTint()
	{
		tinting = false;
		base.sprite.GetComponent<SpriteRenderer>().color = Color.white;
	}
}
