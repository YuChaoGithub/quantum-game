using UnityEngine;
using System.Collections;

public class SBalloon : MonoBehaviour 
{
	public float interval = 2f;

	public GameObject waste;

	private Animator animator;

	private const float EnlargeTime = .6667f;
	private const float ShrinkTime = .41667f;
	private const float WasteContinueTime = .5f;
	void Start()
	{
		animator = GetComponent<Animator>();
		StartCoroutine(Cycle());
	}

	IEnumerator Cycle()
	{
		while (true)
		{
			//wait for the interval
			yield return new WaitForSeconds(interval);

			animator.SetBool("Enlarge", true);
			yield return new WaitForSeconds(EnlargeTime);

			//shrink
			waste.SetActive(true);
			yield return new WaitForSeconds(ShrinkTime + WasteContinueTime);

			waste.SetActive(false);
			animator.SetBool("Enlarge", false);
		}
	}

}
