using UnityEngine;
using System.Collections;

public class HeBossTrigger : BossTrigger
{
	public GameObject[] deleteStuffs;

	protected override void OnTriggerEnter2D(Collider2D col)
	{
		base.OnTriggerEnter2D(col);
		if (col.gameObject.tag == "Player")
		{
			foreach (GameObject go in deleteStuffs) go.SetActive(false);
			base.boss.GetComponent<Helium>().StartMovement();
			Destroy(gameObject);
		}
	}

}
