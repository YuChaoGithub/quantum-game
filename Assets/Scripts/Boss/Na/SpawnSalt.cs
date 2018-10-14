using UnityEngine;
using System.Collections;

public class SpawnSalt : MonoBehaviour 
{
	public GameObject salt;

	public GameObject[] spawnPosition;
	public GameObject audio;

	private const float SpawnInterval = 1f;

	void Start()
	{
		InvokeRepeating ("Spawn", SpawnInterval, SpawnInterval);
	}

	void Spawn()
	{
		Instantiate(audio, transform.position, Quaternion.identity);
		foreach (GameObject pos in spawnPosition) 
		{
			Instantiate(salt, pos.transform.position, Quaternion.identity);
		}
	}
}
