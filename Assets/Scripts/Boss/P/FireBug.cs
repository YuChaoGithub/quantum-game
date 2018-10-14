using UnityEngine;
using System.Collections;

public class FireBug : Enemy
{
	public override void BeingHit ()
	{
		base.BeingHit ();
		Destroy(gameObject);
	}
}
