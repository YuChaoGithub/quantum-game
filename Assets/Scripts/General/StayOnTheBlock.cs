using UnityEngine;
using System.Collections;

public class StayOnTheBlock : MonoBehaviour 
{

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player" || col.gameObject.tag == "Monster")
		{
			col.transform.parent = gameObject.transform;
		}
	}
	
	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player" || col.gameObject.tag == "Monster")
		{
			col.transform.parent = null;
		}
	}
}
