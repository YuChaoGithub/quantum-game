using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour 
{

	private float duration;
	private float time;

	public GameObject explodeAudio;

	void Start () 
	{
		duration = 0.2f;
		time = Time.timeSinceLevelLoad;

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Time.timeSinceLevelLoad - time > duration)
		{
			Instantiate(explodeAudio, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
