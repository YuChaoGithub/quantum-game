using UnityEngine;
using System.Collections;

public class CloudBlock : MonoBehaviour 
{

	private Animator animator;
	public GameObject audio;
	
	private const float ReappearTime = 3f;
	private const float DissolveAnimationTime = 0.65f;

	void Start()
	{
		animator = GetComponent<Animator>();
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Player") 
		{
			StartCoroutine(Dissolve());
		}
	}

	IEnumerator Dissolve()
	{
		//start dissolve
		animator.SetBool("Dissolve", true);
		Instantiate(audio, transform.position, Quaternion.identity);
		yield return new WaitForSeconds(DissolveAnimationTime);

		//delete the collider
		GetComponent<EdgeCollider2D>().isTrigger = true;

		//reappear
		yield return new WaitForSeconds(ReappearTime);
		GetComponent<EdgeCollider2D>().isTrigger = false;
		animator.SetBool("Dissolve", false);
	}
}
