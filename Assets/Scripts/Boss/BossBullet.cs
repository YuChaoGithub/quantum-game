using UnityEngine;
using System.Collections;

public class BossBullet : Bullet
{
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			col.gameObject.GetComponent<Player>().Hurt(base.damage);
			base.DestroyBullet();
		}
		else if (col.gameObject.tag == "Player Bullet")
		{
			//destroy player bullet
			col.gameObject.GetComponent<Bullet>().DestroyBullet();

			//self destroy
			base.DestroyBullet();
		}
		else if (col.gameObject.tag == "Player Knife")
		{
			col.gameObject.GetComponent<Knife>().DestroyKnife();

			base.DestroyBullet();
		}
		else if (col.gameObject.tag == "Float Player")
		{
			col.gameObject.GetComponent<FloatPlayer>().Hurt(base.damage);
		}
	}
}
