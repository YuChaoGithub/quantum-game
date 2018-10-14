using UnityEngine;
using System.Collections;

public class TransportingCurrent : MonoBehaviour 
{
	public GameObject appearPos;
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			Vector3 newPos = appearPos.transform.position;
			col.gameObject.transform.position = newPos;
		}
	}
}
