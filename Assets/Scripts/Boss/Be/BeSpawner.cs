using UnityEngine;
using System.Collections;

public class BeSpawner : MonoBehaviour 
{
	public GameObject spawnPos;
	public GameObject beBall;
	public GameObject audio;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			Instantiate(beBall, spawnPos.transform.position, Quaternion.identity);
			Instantiate(audio, spawnPos.transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
