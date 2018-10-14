using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour 
{
	public GameObject[] prefabs;
	public float interval = 5f;
	public int garbageCapacity = 1000;
	public GameObject audio;

	static int amount = 0;

	void Start()
	{
		InvokeRepeating("Generate", interval, interval);
	}

	void Generate()
	{
		if (amount > garbageCapacity) {
			CancelInvoke("Generate");
		}
		int index = Random.Range(0, prefabs.Length - 1);
		Instantiate(prefabs[index], transform.position, Quaternion.identity);
		Instantiate(audio, transform.position, Quaternion.identity);
		amount++;
	}
}
