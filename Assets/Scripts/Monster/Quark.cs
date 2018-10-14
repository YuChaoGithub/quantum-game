using UnityEngine;
using System.Collections;

public class Quark : Enemy
{
	public GameObject ball;

	public override void BeingHit()
	{
		base.BeingHit();
		Instantiate(ball,transform.position,Quaternion.identity);
		Destroy(gameObject);
	}
}
