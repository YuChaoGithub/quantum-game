using UnityEngine;
using System.Collections;

public class DisappearTrigger : MonoBehaviour 
{
	public GameObject[] stuffs;
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag != "Player")
			return;
		
		foreach (GameObject stuff in stuffs)
		{
			stuff.SetActive(false);
		}
	}
}
