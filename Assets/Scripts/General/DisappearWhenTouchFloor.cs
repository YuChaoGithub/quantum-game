using UnityEngine;
using System.Collections;

public class DisappearWhenTouchFloor : MonoBehaviour 
{
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Ground")
		{
			Destroy(gameObject);
		}
	}
}
