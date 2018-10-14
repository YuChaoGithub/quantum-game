using UnityEngine;
using System.Collections;

public class KBlock : MonoBehaviour 
{
	public GameObject waterAudio;
	public GameObject explosionAudio;

	public GameObject injector;
	public GameObject explosion;
	public GameObject particles;
	public GameObject waterDrop;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			InjectWater();
		}
		else if (col.gameObject.tag == "Water Drop")
		{
			Explode();
		}
	}

	void Explode()
	{
		Instantiate(explosionAudio, transform.position, Quaternion.identity);
		explosion.SetActive(true);
		particles.SetActive(true);
		Destroy(waterDrop);
		Destroy(gameObject);
	}

	void InjectWater()
	{
		injector.GetComponent<Animator>().SetTrigger("Trigger");
		Instantiate(waterAudio, transform.position, Quaternion.identity);
		waterDrop.SetActive(true);
	}
}
