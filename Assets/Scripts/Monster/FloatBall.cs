using UnityEngine;
using System.Collections;

public class FloatBall : FloatEnemy
{
	public float speed;
	public float spinDir;
	public GameObject sprite;

	private const float RotateCoefficient = 140f;

	void Update()
	{
		transform.Translate(0f, Time.deltaTime * speed, 0f);
		sprite.transform.Rotate(0f, 0f, Time.deltaTime * RotateCoefficient * spinDir);
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		ChangeSide();
	}

	void ChangeSide()
	{
		speed *= -1;
	}

	public override void BeingHit()
	{
		Destroy(gameObject);
	}
}
