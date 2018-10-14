using UnityEngine;
using System.Collections;

public class BBullet : MonoBehaviour 
{
	public float xRandomRange;
	public float yRandomRange;
	public void Shoot() 
	{
		GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-xRandomRange, xRandomRange), Random.Range(0f, yRandomRange)));
	}
}
