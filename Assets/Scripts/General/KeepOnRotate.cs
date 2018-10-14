using UnityEngine;
using System.Collections;

public class KeepOnRotate : MonoBehaviour 
{
	public float rotateSpeed;

	void Update()
	{
		transform.Rotate(new Vector3(0f, 0f, rotateSpeed * Time.deltaTime));
	}
}
