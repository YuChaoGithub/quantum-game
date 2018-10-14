using UnityEngine;
using System.Collections;



//prepare -> shoot -> interval

public class PhotonGun : MonoBehaviour
{
	public GameObject photon;
	public GameObject shootPosition;
	public float interval;
	public float side;

	public GameObject audio;

	private Animator animator;
	private bool preparing;
	private float intervalTimer;
	private float preparingTimer;
	//private int count = 0;

	private const float PrepareShootTime = 2f;

	void Start()
	{
		animator = GetComponent<Animator>();
		Prepare();
	}

	void Update()
	{
		if (preparing && Time.timeSinceLevelLoad - preparingTimer > PrepareShootTime)
		{
			Shoot();
		}
		else if (!preparing && Time.timeSinceLevelLoad - intervalTimer > interval)
		{
			Prepare();
		}
	}

	void Shoot()
	{
		preparing = false;
		animator.SetBool("Preparing", false);
		intervalTimer = Time.timeSinceLevelLoad;

		GameObject bullet = (Instantiate(photon, shootPosition.transform.position, Quaternion.identity)) as GameObject;
		bullet.GetComponent<Bullet>().side = side;
	}

	void Prepare()
	{
		preparing = true;
		animator.SetBool("Preparing", true);
		Instantiate(audio, transform.position, Quaternion.identity);
		preparingTimer = Time.timeSinceLevelLoad;
	}
}
