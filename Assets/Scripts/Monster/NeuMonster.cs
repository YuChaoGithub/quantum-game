using UnityEngine;
using System.Collections;

public class NeuMonster : Enemy
{
	public GameObject upBall;
	public GameObject downBall;

	public override void BeingHit()
	{
		base.BeingHit();
		Instantiate(upBall,transform.position,Quaternion.identity);
		Instantiate(downBall,transform.position,Quaternion.identity);
		Instantiate(downBall,transform.position,Quaternion.identity);
		Destroy(gameObject);
	}
}
