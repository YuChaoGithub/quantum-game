using UnityEngine;
using System.Collections;

public class BeltEffector : MonoBehaviour 
{
	public bool limitVelocity = true;

	private const float VelocityCap = 5f;

	void OnCollisionStay2D(Collision2D col)
	{
		if (!limitVelocity) return;

		if (col.gameObject.tag == "Bottle")
		{
			Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();

			if (rb.velocity.x > VelocityCap)
			{
				rb.velocity = new Vector3(VelocityCap, rb.velocity.y);
			}
			else if (rb.velocity.x < -VelocityCap)
			{
				rb.velocity = new Vector3(-VelocityCap, rb.velocity.y);
			}
		}
	}
}
