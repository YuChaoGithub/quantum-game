using UnityEngine;
using System.Collections;

public class HeExplosion : MonoBehaviour 
{
	public GameObject audio;

	private const float ExplodeTime = 0.17f;

	void Start()
	{
		Instantiate(audio, transform.position, Quaternion.identity);
		Invoke("DestroyExplosion", ExplodeTime);
	}

	void DestroyExplosion()
	{
		Destroy(gameObject);
	}
}
