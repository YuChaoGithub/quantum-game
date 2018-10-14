using UnityEngine;
using System.Collections;

public class LightTube : MonoBehaviour 
{
	private Animator animator;

	public GameObject leftBulletPrefab;
	public GameObject rightBulletPrefab;
	public int side;
	public float interval;
	public GameObject shootPos;

	private const float AnimationTime = 0.666667f;
	public GameObject shootSound;

	void Start()
	{
		animator = GetComponent<Animator>();
		StartCoroutine(Cycle());
	}

	IEnumerator Cycle()
	{
		while (true)
		{
			animator.SetTrigger("Shoot");
			yield return new WaitForSeconds(AnimationTime);
			Instantiate(side == -1 ? leftBulletPrefab : rightBulletPrefab, shootPos.transform.position, Quaternion.identity);
			Instantiate(shootSound, transform.position, Quaternion.identity);

			yield return new WaitForSeconds(interval);
		}
	}
}
