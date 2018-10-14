using UnityEngine;
using System.Collections;

public class salt : MonoBehaviour 
{
	private const float ForceScale = 10f;
	private const float RotationScale = 20f;
	private const float DissappearTime = 7f;	
	void Start()
	{
		GetComponent<Rigidbody2D> ().AddForce (new Vector3(Random.Range (-ForceScale, ForceScale),Random.Range (0, ForceScale)));
		GetComponent<Rigidbody2D>().AddTorque(Random.Range (-RotationScale, RotationScale));

		Invoke ("Remove", DissappearTime);
	}

	void Remove()
	{
		Destroy (gameObject);
	}
}
