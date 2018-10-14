using UnityEngine;
using System.Collections;

public class TrafficLight : MonoBehaviour 
{
	public float redLightDuration;
	public float greenLightDuration;

	public GameObject redLightAudio;
	public GameObject greenLightAudio;
	public GameObject yellowLightAudio;

	public GameObject collider;

	private Animator animator;

	private const float YellowLightDuration = .5f;

	void Start()
	{
		animator = GetComponent<Animator>();
		StartCoroutine(Cycle());
	}

	IEnumerator Cycle()
	{
		while (true)
		{
			//red
			yield return new WaitForSeconds(redLightDuration);

			animator.SetTrigger("Green");
			Instantiate(greenLightAudio, transform.position, Quaternion.identity);
			collider.SetActive(false);

			//green
			yield return new WaitForSeconds(greenLightDuration);

			animator.SetTrigger("Yellow");
			Instantiate(yellowLightAudio, transform.position, Quaternion.identity);

			//yellow
			yield return new WaitForSeconds(YellowLightDuration);

			animator.SetTrigger("Red");
			Instantiate(redLightAudio, transform.position, Quaternion.identity);
			collider.SetActive(true);

		}
	}

}
