using UnityEngine;
using System.Collections;

public class PlayAudioWhenHit : MonoBehaviour 
{
	public string hitTag1;
	public string hitTag2;
	public GameObject audio;

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == hitTag1 || col.gameObject.tag == hitTag2)
			Instantiate(audio, transform.position, Quaternion.identity);
	}
}
