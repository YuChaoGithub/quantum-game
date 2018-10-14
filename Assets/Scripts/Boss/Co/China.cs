using UnityEngine;
using System.Collections;

public class China : MonoBehaviour
{
	public GameObject explosion;
	public GameObject explosionPoint;
	public float damage = 10f;
	public GameObject audio;

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Player") col.gameObject.GetComponent<Player>().Hurt(damage);
		Instantiate(audio, transform.position, Quaternion.identity);
		Instantiate(explosion, explosionPoint.transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}
