using UnityEngine;
using System.Collections;

public class InvisibleWallForMonster : MonoBehaviour 
{
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Monster")
		{
			col.gameObject.GetComponent<Enemy>().ChangeSide();
		}
	}
}
