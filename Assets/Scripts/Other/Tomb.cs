using UnityEngine;
using System.Collections;

public class Tomb : MonoBehaviour 
{
	public GameObject startPos;
	public GameObject endPos;
	public GameObject fire;
	public GameObject audio;

	public float interval = 2f;
	public float animDuration = 0.5f;
	public float stopDuration = 0.5f;

	private Animator anim;
	private Vector3 start;
	private Vector3 end;

	private const float IndicateTime = 0.5f;

	void Start()
	{
		anim = GetComponent<Animator>();
		start = startPos.transform.position;
		end = endPos.transform.position;

		StartCoroutine(Cycle());
	}
	
	IEnumerator Cycle()
	{
		while (true)
		{
			//interval
			yield return new WaitForSeconds(interval - IndicateTime);

			//turn red
			anim.SetBool("on", true);
			Instantiate(audio, transform.position, Quaternion.identity);

			yield return new WaitForSeconds(IndicateTime);

			//drop
			fire.SetActive(true);
			fire.transform.position = start;
			StartCoroutine(FireDrop());

			yield return new WaitForSeconds(animDuration + stopDuration);

			//back to interval
			fire.SetActive(false);
			anim.SetBool("on", false);
		}
	}

	IEnumerator FireDrop()
	{
		float i = 0f;
		float scale = 1f/animDuration;

		while (i < 1f)
		{
			i += Time.deltaTime * scale;
			float newY = Mathf.Lerp(start.y, end.y, i);
			Vector3 newPos = new Vector3(fire.transform.position.x, newY);
			fire.transform.position = newPos;
			yield return null;
		}
	}
}
