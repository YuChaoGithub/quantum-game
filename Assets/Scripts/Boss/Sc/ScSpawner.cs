using UnityEngine;
using System.Collections;

public class ScSpawner : MonoBehaviour 
{
	public GameObject ScBall;
	public GameObject spawnPos;
	public GameObject spawnAudio;
	public float interval = 1.5f;
	public int side = -1;

	void Start()
	{
		InvokeRepeating("Spawn", interval, interval);
	}

	void Spawn()
	{
		GameObject ball = (Instantiate(ScBall, spawnPos.transform.position, Quaternion.identity)) as GameObject;
		ball.GetComponent<ScBall>().side = side;
		Instantiate(spawnAudio, transform.position, Quaternion.identity);
	}
}
