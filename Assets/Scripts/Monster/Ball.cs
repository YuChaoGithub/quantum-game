using UnityEngine;
using System.Collections;

public class Ball : Enemy
{
	public GameObject sprite;
	
	private const float RotateCoefficient = 70f;
	
	protected override void Update()
	{
		base.Update();
		sprite.transform.Rotate(new Vector3(0f,0f, -1f * base.speed * base.side * RotateCoefficient * Time.deltaTime));
	}
	
	public override void ChangeSide()
	{
		base.side *= -1;
	}

	public override void BeingHit()
	{
		base.BeingHit();
		Destroy(gameObject);
	}
}
