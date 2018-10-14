using UnityEngine;
using System.Collections;

public class AsBomb : MonoBehaviour 
{
	public float damage;
	public GameObject explosionPrefab;
	public GameObject explosionArea;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			col.gameObject.GetComponent<Player>().Hurt(damage);
			Instantiate(explosionArea, transform.position, Quaternion.identity);
			Instantiate(explosionPrefab, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
