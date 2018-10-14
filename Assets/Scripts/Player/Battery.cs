using UnityEngine;
using System.Collections;

public class Battery : MonoBehaviour 
{
	public int type; //0 for negative, 1 for positive
	public int recoverAmount = 50;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			if (type == col.gameObject.GetComponent<Player>().particleType)
			{
				col.gameObject.GetComponent<Player>().Recover(recoverAmount);
				Destroy(gameObject);
			}
		}
	}
}
