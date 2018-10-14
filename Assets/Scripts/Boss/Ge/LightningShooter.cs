using UnityEngine;
using System.Collections;

public class LightningShooter : MonoBehaviour 
{
	public GameObject shootPostion;
	public GameObject lightningPrefab;
	public GameObject audio;

	private const float AnimationTime = 0.5f;

	public void Shoot()
	{
		GetComponent<Animator>().SetTrigger("Shoot");
		Instantiate(audio, transform.position, Quaternion.identity);
		Invoke("ShootLightning", AnimationTime);
	}

	void ShootLightning()
	{
		Instantiate(lightningPrefab, shootPostion.transform.position, Quaternion.identity);
	}
}
