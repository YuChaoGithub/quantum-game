using UnityEngine;
using System.Collections;

public class LaserBeam : MonoBehaviour 
{
	void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			col.gameObject.GetComponent<Player>().HitByLaser();
		}
		else if (col.gameObject.tag == "Float Player")
		{
			col.gameObject.GetComponent<FloatPlayer>().HitByLaser();
		}
	}
}
