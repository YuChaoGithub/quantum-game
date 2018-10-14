using UnityEngine;
using System.Collections;

public class FreezingBlock : MonoBehaviour 
{
	void OnCollisionStay2D(Collision2D col)
	{
		if (col.gameObject.tag == "Player") 
		{
			col.gameObject.GetComponent<Player>().Freeze();
		}
	}
}
