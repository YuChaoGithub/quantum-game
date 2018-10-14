using UnityEngine;
using System.Collections;

public class LightControl : MonoBehaviour 
{
	public float onTime = 3f;
	public float offTime = 3f;

	public GameObject lightSource;
	public GameObject audio;

	private const float Delay = .2f;
	private Animator animator;

	void Start()
	{
		animator = GetComponent<Animator>();
		StartCoroutine(ControlSwitch());
	}

	IEnumerator ControlSwitch()
	{
		while (true)
		{
			yield return new WaitForSeconds(offTime);

			animator.SetBool("On", false);
			Instantiate(audio, transform.position, Quaternion.identity);
			yield return new WaitForSeconds(Delay);
			lightSource.SetActive(false);

			yield return new WaitForSeconds(onTime);

			animator.SetBool("On", true);
			Instantiate(audio, transform.position, Quaternion.identity);
			yield return new WaitForSeconds(Delay);
			lightSource.SetActive(true);
		}
	}
}
