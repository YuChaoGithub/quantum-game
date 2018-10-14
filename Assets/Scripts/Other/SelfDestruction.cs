using UnityEngine;
using System.Collections;

public class SelfDestruction : MonoBehaviour 
{
	public float time;

	void Start()
	{
		Invoke("DestroySelf", time);
	}

	void DestroySelf()
	{
		Destroy(gameObject);
	}
}
