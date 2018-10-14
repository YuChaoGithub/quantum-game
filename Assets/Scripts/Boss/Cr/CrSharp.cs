using UnityEngine;
using System.Collections;

public class CrSharp : MonoBehaviour 
{

	private const float Damage = 10f;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			col.gameObject.GetComponent<Player>().Hurt(Damage);
		}
	}
}
