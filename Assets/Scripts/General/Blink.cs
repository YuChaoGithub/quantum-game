using UnityEngine;
using System.Collections;

public class Blink : MonoBehaviour 
{
	public GameObject blinkObj;
	public float interval;
	public float randomCap;

	public GameObject audio;

	void Start()
	{
		StartCoroutine(BlinkCycle());
	}

	IEnumerator BlinkCycle()
	{
		while (true)
		{
			yield return new WaitForSeconds(interval + Random.Range(-randomCap, randomCap));
			blinkObj.SetActive(true);
			Instantiate(audio, transform.position, Quaternion.identity);

			yield return new WaitForSeconds(interval + Random.Range(-randomCap, randomCap));
			blinkObj.SetActive(false);
		}
	}
}
