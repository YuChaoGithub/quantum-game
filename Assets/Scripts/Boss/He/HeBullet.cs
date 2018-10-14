using UnityEngine;
using System.Collections;

public class HeBullet : MonoBehaviour 
{
	public GameObject explosion;
	public GameObject theCollider;

	private const float stopTime = 1.2f;
	private const float damage = 10f;

	void Start()
	{
		Invoke("StopMoving", stopTime);
	}

	void StopMoving()
	{
		Destroy(theCollider);
		GetComponent<Rigidbody2D>().isKinematic = true;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			//explode
			Instantiate(explosion, transform.position, Quaternion.identity);
			col.gameObject.GetComponent<Player>().Hurt(damage);
			Destroy(gameObject);
		}
	}
}
