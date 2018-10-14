using UnityEngine;
using System.Collections;

public class DeleteGarbage : MonoBehaviour 
{
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Garbage")
		{
			Destroy(col.gameObject);
		}
	}
}
