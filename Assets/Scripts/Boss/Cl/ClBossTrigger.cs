using UnityEngine;
using System.Collections;

public class ClBossTrigger : BossTrigger
{

	public GameObject block;

	protected override void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Float Player")
		{
			base.boss.SetActive(true);
			block.SetActive(true);
			Destroy(gameObject);
		}
	}
}
