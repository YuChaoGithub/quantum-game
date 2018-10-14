using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerBullet : Bullet 
{

	protected override void Start()
	{
		base.Start();

	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Monster")
		{
			col.gameObject.GetComponent<Enemy>().BeingHit();
			Destroy(gameObject);
		}
		else if (col.gameObject.tag == "Boss")
		{
			col.gameObject.GetComponent<Boss>().TakeDamage(base.damage);
			Destroy(gameObject);
		}
		else if (col.gameObject.tag == "Wall")
		{
			Destroy(gameObject);
		}
		else if (col.gameObject.tag == "Float Monster")
		{
			col.gameObject.GetComponent<FloatEnemy>().BeingHit();
			Destroy(gameObject);
		}
		else if (col.gameObject.tag == "Kr Small Boss")
		{
			col.gameObject.GetComponent<KrSmallBoss>().BeingHit();
			Destroy(gameObject);
		}
	}
	
}
