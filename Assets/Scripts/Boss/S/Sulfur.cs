using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sulfur : Boss
{
	public GameObject[] wastes;

	public GameObject beingHitAudio;
	public GameObject spinAudio;

	private const float LeftBound = 230f;
	private const float TopBound = 16.5f;
	private const float RightBound = 244.5f;
	private const float BottomBound = 14.5f;
	private const float WasteAppearInterval = 0.05f;
	private const float MovementSpeed = 2f;
	private const float StopTime = 0.5f;
	private bool tinting = false;
	private const float TintDuration = 0.1f;

	protected override void Start ()
	{
		base.Start ();

		StartCoroutine(Cycle());
	}

	protected override void Die ()
	{
		base.Die ();

		base.bottle.transform.position = transform.position;
		Destroy(gameObject);
	}

	public override void TakeDamage(int damage)
	{
		if (health == 0) return;

		base.TakeDamage(damage);

		Instantiate(beingHitAudio, transform.position, Quaternion.identity);
		if (!tinting) Tint();
	}

	void Tint()
	{
		tinting = true;
		base.sprite.GetComponent<SpriteRenderer>().color = Color.red;

		Invoke("RecoverTint", TintDuration);
	}

	void RecoverTint()
	{
		tinting = false;
		base.sprite.GetComponent<SpriteRenderer>().color = Color.white;
	}

	IEnumerator Cycle()
	{
		while (base.health > 0f)
		{
			//move to objective
			Vector3 newPos = new Vector3(Random.Range(LeftBound, RightBound), Random.Range(BottomBound, TopBound), 0f);
			float time = Vector3.Distance(transform.position, newPos) / MovementSpeed;
			StartCoroutine(MoveToObjective(transform.position, newPos, time));
			Instantiate(spinAudio, transform.position, Quaternion.identity);
			yield return new WaitForSeconds(time);

			//stop and release waste
			yield return new WaitForSeconds(StopTime);
			List<GameObject> wasteList = new List<GameObject>();
			foreach (GameObject waste in wastes)
			{
				wasteList.Add(waste);
			}
			while (wasteList.Count > 0)
			{
				int index = Random.Range(0, wasteList.Count);
				wasteList[index].SetActive(true);
				wasteList.RemoveAt(index);
				yield return new WaitForSeconds(WasteAppearInterval);
			}

			yield return new WaitForSeconds(StopTime);

			foreach (GameObject waste in wastes)
			{
				waste.SetActive(false);
			}
		}
	}
		

	IEnumerator MoveToObjective(Vector3 init, Vector3 final, float time)
	{
		float i = 0f;
		while (i <= 1)
		{
			i += Time.deltaTime/time;

			//position
			Vector3 newPos = Vector3.Lerp(init, final, i);
			transform.position = newPos;

			//rotation
			base.sprite.transform.localEulerAngles = new Vector3(0f, 0f, Mathf.Lerp(0f, 360f, i));

			yield return null;
		}
	}
}
