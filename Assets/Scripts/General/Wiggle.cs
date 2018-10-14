using UnityEngine;
using System.Collections;

public class Wiggle : MonoBehaviour 
{
	public float wiggleOffset = 0.1f;
	public GameObject startPos;

	//private float timestamp = 0f;

	//private const float WiggleInterval = 0.05f;

	void Update()
	{
		//if (Time.timeSinceLevelLoad - timestamp < WiggleInterval) return;

		//timestamp = Time.timeSinceLevelLoad;

		float xOffset = Random.Range(-wiggleOffset, wiggleOffset);
		transform.position = new Vector3(xOffset + startPos.transform.position.x, transform.position.y);
	}
}
