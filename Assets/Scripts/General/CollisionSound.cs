using UnityEngine;
using System.Collections;

public class CollisionSound : MonoBehaviour 
{
	public GameObject audio;

	void OnCollisionEnter2D(Collision2D col)
	{
		Instantiate(audio, transform.position, Quaternion.identity);
	}
}
