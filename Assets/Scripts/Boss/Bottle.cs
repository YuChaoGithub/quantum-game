using UnityEngine;
using System.Collections;

public class Bottle : MonoBehaviour 
{
	public GameObject levelCompleteRoute;
	public GameObject audio;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player" || col.gameObject.tag == "Float Player")
		{
			levelCompleteRoute.SetActive(true);
			Instantiate(audio, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
