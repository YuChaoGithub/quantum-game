using UnityEngine;
using System.Collections;

public class Bounce : MonoBehaviour 
{
	public GameObject sprite;
	public GameObject bounceAudio;

	void OnCollisionEnter2D(Collision2D col)
	{
		sprite.GetComponent<Animator>().SetTrigger("Bounce");
		Instantiate(bounceAudio, transform.position, Quaternion.identity);
	}
}
