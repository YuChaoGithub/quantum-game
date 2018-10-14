using UnityEngine;
using System.Collections;

public class Nickel : Boss 
{
	public GameObject beingHitAudio;
	private const float RotatingSpeed = 500f;
	private bool tinting = false;
	private const float TintDuration = 0.2f;

	protected override void Die ()
	{
		base.Die ();

		Destroy(gameObject);
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

	void Update()
	{
		transform.Rotate(new Vector3(0f, 0f, Random.Range(0f, RotatingSpeed) * Time.deltaTime));
	}
}
