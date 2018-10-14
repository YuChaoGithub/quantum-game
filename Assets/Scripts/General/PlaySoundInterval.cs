using UnityEngine;
using System.Collections;

public class PlaySoundInterval : MonoBehaviour
{
	public GameObject audio;
	public float interval;
	private float timestamp;

	void Start () 
	{
		timestamp = Time.timeSinceLevelLoad;
	}
	
	void Update () 
	{
		if (Time.timeSinceLevelLoad - timestamp > interval)
		{
			Instantiate(audio, transform.position, Quaternion.identity);
			timestamp = Time.timeSinceLevelLoad;
		}
	}
}
