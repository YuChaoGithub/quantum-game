using UnityEngine;
using System.Collections;

public class OMonster : MonoBehaviour 
{
	public Vector3 goal;
	public float moveTime = 2f;
	public float stopTime = 0.5f;
	public float fireInterval = 2f;
	public float fireDuration = 2f;

	public GameObject fire;

	public GameObject warningAudio;

	private Vector3 initialPosition;
	private int rotateDir;
	private Animator animator;

	private Vector3 RotationInitialPosition;
	private Vector3 RotationGoal;
	private float rotationTime = 1.5f;

	private const float WarningTime = 0.75f;
	void Start()
	{
		RotationInitialPosition = new Vector3(0, 0, -25);
		RotationGoal = new Vector3(0, 0, 25);

		rotateDir = Random.Range(-2, 2) > 0 ? -1 : 1;
		initialPosition = transform.position;
		animator = GetComponent<Animator>();
		StartCoroutine(Movement());
		StartCoroutine(CycleCoroutine());
		StartCoroutine(RotationCoroutine());
	}

	IEnumerator RotationCoroutine()
	{
		transform.rotation = Quaternion.Euler(RotationInitialPosition);

		float i;
		
		while (true)
		{
			//move to goal
			i = 0f;
			while (i < 1f)
			{
				i += Time.deltaTime / rotationTime;
				Vector3 newRot = Vector3.Lerp(RotationInitialPosition, RotationGoal, i);
				transform.rotation = Quaternion.Euler(newRot);
				yield return null;
			}
			
			//back to initial spot
			i = 0f;
			while (i < 1f)
			{
				i += Time.deltaTime / rotationTime;
				Vector3 newRot = Vector3.Lerp(RotationGoal, RotationInitialPosition, i);
				transform.rotation = Quaternion.Euler(newRot);
				yield return null;
			}

		}
		
	}

	IEnumerator CycleCoroutine()
	{
		while (true)
		{
			yield return new WaitForSeconds(fireInterval);

			//warning
			animator.SetBool("Burn", true);
			Instantiate(warningAudio, transform.position, Quaternion.identity);
			yield return new WaitForSeconds(WarningTime);

			//I set FIRE to the O2, watch it burn..
			fire.SetActive(true);
			rotationTime = 0.4f;
			yield return new WaitForSeconds(fireDuration);

			//recover
			fire.SetActive(false);
			rotationTime = 1.5f;
			animator.SetBool("Burn", false);
		}
	}

	IEnumerator Movement()
	{
		float i;
		
		while (true)
		{
			//move to goal
			i = 0f;
			while (i < 1f)
			{
				i += Time.deltaTime / moveTime;
				Vector3 newPos = Vector3.Lerp(initialPosition, goal, i);
				transform.position = newPos;
				yield return null;
			}
			
			//reached goal
			yield return new WaitForSeconds(stopTime);
			
			//back to initial spot
			i = 0f;
			while (i < 1f)
			{
				i += Time.deltaTime / moveTime;
				Vector3 newPos = Vector3.Lerp(goal, initialPosition, i);
				transform.position = newPos;
				yield return null;
			}
			
			//reached initial spot
			yield return new WaitForSeconds(stopTime);
		}
		
	}
}
