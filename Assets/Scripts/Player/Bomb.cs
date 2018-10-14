using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour 
{
	public float damage;
	public float explosionTime;
	public GameObject explosion;

	private float explosionTimer;

	void Start()
	{
		explosionTimer = Time.timeSinceLevelLoad;
	}

	void Update()
	{
		if (Time.timeSinceLevelLoad - explosionTimer > explosionTime)
		{
			Explode();
		}
	}

	void Explode()
	{
		Instantiate(explosion,transform.position,Quaternion.identity);
		Destroy(gameObject);
	}
}
