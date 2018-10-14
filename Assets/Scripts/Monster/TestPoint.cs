using UnityEngine;
using System.Collections;

public class TestPoint : MonoBehaviour 
{
	private bool grounded; 
	private bool wallHit;

	void FixedUpdate()
	{
		grounded = false;
		wallHit = false;
	}

	void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.tag == "Wall" || col.gameObject.tag == "Monster") wallHit = true;
		if (col.gameObject.tag == "Ground") grounded = true; 
	}

	public bool WallHit()
	{
		return wallHit;
	}

	public bool Grounded()
	{
		return grounded;
	}
}
