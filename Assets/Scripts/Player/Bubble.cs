using UnityEngine;
using System.Collections;

public class Bubble : MonoBehaviour 
{
	public float driftSpeed = -5f;
	public float driftRange = 0.3f;

	void Update()
	{
		transform.Translate(Random.Range(-driftRange, driftRange) , driftSpeed * Time.deltaTime, 0f);
	}
}
