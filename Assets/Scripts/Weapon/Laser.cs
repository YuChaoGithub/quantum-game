using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour 
{
	public float laserTime = 1f;
	public float interval = 2f;

	public GameObject beam;

	private Animator animator;

	private const float WarningTime = 0.7f;
	private const float FadingTime = 0.2f;

	void Start()
	{
		animator = GetComponent<Animator>();
		StartCoroutine(CycleCoroutine());
	}

	IEnumerator CycleCoroutine()
	{
		while (true)
		{
			//waring state
			yield return new WaitForSeconds(WarningTime);

			animator.SetTrigger("Fade In");

			//Fading in
			yield return new WaitForSeconds(FadingTime);

			//activated
			ActivateCollider();
			yield return new WaitForSeconds(laserTime);
			animator.SetTrigger("Fade Out");

			//Fading out
			yield return new WaitForSeconds(FadingTime);

			//deactivated
			DeactivateCollider();
			yield return new WaitForSeconds(interval);
			animator.SetTrigger("To Prepare");
		}
	}

	void ActivateCollider()
	{
		beam.SetActive(true);
	}

	void DeactivateCollider()
	{
		beam.SetActive(false);
	}
}
