using UnityEngine;
using System.Collections;

public class AlternateSquare : MonoBehaviour 
{
	public GameObject posField;
	public GameObject negField;

	public float stayTime = 3f;
	
	private Animator anim;

	private const float AlternateAnimationTime = 1.833f;

	void Start()
	{
		anim = GetComponent<Animator>();

		StartCoroutine(alternate());
	}

	IEnumerator alternate()
	{
		while (true)
		{
			//neg
			negField.SetActive(true);
			posField.SetActive(false);
			yield return new WaitForSeconds(stayTime);

			anim.SetTrigger("Alternate");
			yield return new WaitForSeconds(AlternateAnimationTime);

			//pos
			posField.SetActive(true);
			negField.SetActive(false);
			yield return new WaitForSeconds(stayTime);

			anim.SetTrigger("Alternate");
			yield return new WaitForSeconds(AlternateAnimationTime);
		}
	}
}
