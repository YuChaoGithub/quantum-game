using UnityEngine;
using System.Collections;

public class AnimatedHe : MonoBehaviour 
{
	private Vector3 endSpot = new Vector3(7f, 35f);
	private const float MoveTime = 7f;

	void Start()
	{
		StartCoroutine(Movement(transform.position, endSpot, MoveTime));
	}

	IEnumerator Movement(Vector3 init, Vector3 final, float time)
	{
		float i = 0f;
		
		while (i <= 1f)
		{
			i += Time.deltaTime / time;
			Vector3 newPos = Vector3.Lerp(init, final, i);
			transform.position = newPos;
			
			yield return null;
		}

		Destroy(gameObject);

		yield return null;
	}
}
