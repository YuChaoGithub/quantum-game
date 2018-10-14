using UnityEngine;
using System.Collections;

public class DisappearInSeconds : MonoBehaviour 
{
	public float sec = 2f;

	void Start() 
	{
		Invoke("Disappear", sec);
	}

	void Disappear()
	{
		Destroy(gameObject);
	}

}
