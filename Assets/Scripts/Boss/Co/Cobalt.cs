using UnityEngine;
using System.Collections;

public class Cobalt : Boss 
{
	public GameObject beingHitAudio;
	public GameObject spawnAudio;
	public GameObject[] pieces;
	public GameObject[] chinas;
	public GameObject fallPos;
	public GameObject[] breakPoses;
	private const float SpinUpTime = 1f;
	private const float TopLine = 56f;
	private const float BottomLine = 46.8f;
	private const float LeftBound = 359f;
	private const float RightBound = 375f;
	private const float StayTime = 0.5f;
	private const float FallInterval = 0.35f;
	private const int FallTimes = 3;

	protected override void Die ()
	{
		base.Die ();

		Destroy(gameObject);
	}

	public override void TakeDamage (int damage)
	{
		if (base.health == 0) return;
		base.TakeDamage (damage);

		Instantiate(beingHitAudio, transform.position, Quaternion.identity);
		foreach (GameObject pos in breakPoses)
		{
			Instantiate(pieces[Random.Range(0, pieces.Length)], pos.transform.position, Quaternion.identity);
		}
		StartCoroutine(MoveToObjective(transform.position, new Vector3(Random.Range(LeftBound, RightBound), TopLine, 0f), SpinUpTime));
		StartCoroutine(SpinTo(0f, 180f, SpinUpTime));
		Invoke("FallChinas", SpinUpTime);
	}

	void FallChinas()
	{
		StopAllCoroutines();
		InvokeRepeating("Fall", StayTime, FallInterval);
		Invoke("FallsBack", StayTime + FallInterval * FallTimes);
	}

	void Fall()
	{
		Instantiate(spawnAudio, transform.position, Quaternion.identity);
		Instantiate(chinas[Random.Range(0, chinas.Length)], fallPos.transform.position, Quaternion.identity);
	}

	void FallsBack()
	{
		CancelInvoke("Fall");
		StartCoroutine(MoveToObjective(transform.position, new Vector3(Random.Range(LeftBound, RightBound), BottomLine, 0f), SpinUpTime));
		StartCoroutine(SpinTo(180f, 0f, SpinUpTime));
	}

	IEnumerator MoveToObjective(Vector3 init, Vector3 final, float time)
	{
		float i = 0f;
		while (i <= 1)
		{
			i += Time.deltaTime/time;
			Vector3 newPos = Vector3.Lerp(init, final, i);
			transform.position = newPos;
			yield return null;
		}
	}

	IEnumerator SpinTo(float init, float final, float time)
	{
		float i = 0f;
		while (i <= 1)
		{
			i += Time.deltaTime/time;
			Quaternion newQua = Quaternion.Euler(0f, 0f, Mathf.Lerp(init, final, i));
			transform.rotation = newQua;
			yield return null;
		}
	}
}
