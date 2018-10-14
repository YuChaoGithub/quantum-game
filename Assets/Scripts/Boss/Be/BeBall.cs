using UnityEngine;
using System.Collections;

public class BeBall : MonoBehaviour 
{
	public GameObject explosion;
	public GameObject explodeAudio;

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Water Pot")
		{
			Destroy(col.gameObject);

			Instantiate(explosion, transform.position, Quaternion.identity);
			Instantiate(explodeAudio, transform.position, Quaternion.identity);

			Destroy(gameObject);
		}
	}

}
