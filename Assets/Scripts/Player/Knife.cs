using UnityEngine;
using System.Collections;

public class Knife : MonoBehaviour 
{
	public float lifetime;
	public int damage = 15;

	private float instantiationTime;
	private bool effective = true;

	void Start()
	{

		//record initial time
		instantiationTime = Time.timeSinceLevelLoad;

		//score
	}

	void Update()
	{
		if (Time.timeSinceLevelLoad - instantiationTime > lifetime)
		{
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (!effective) return;

		if (col.gameObject.tag == "Monster")
		{
			col.gameObject.GetComponent<Enemy>().BeingHit();
			Destroy(gameObject);
		}
		else if (col.gameObject.tag == "Boss")
		{
			col.gameObject.GetComponent<Boss>().TakeDamage(damage);
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

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Ground")
		{
			effective = false;
		}
	}

	public void DestroyKnife()
	{
		Destroy(gameObject);
	}
}

