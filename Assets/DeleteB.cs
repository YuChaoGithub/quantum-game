using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeleteB : MonoBehaviour
{
	public static List<GameObject> boronList = new List<GameObject>();

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			foreach (GameObject go in boronList)
				Destroy(go);
		}
	}
}
