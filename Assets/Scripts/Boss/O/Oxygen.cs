using UnityEngine;
using System.Collections;

public class Oxygen : Boss
{
	private Animator animator;

	//for shooting
	public GameObject firePrefabs;
	public GameObject shootPosition;
	private const float ShootAnimationTime = 0.5f;

	//audio
	public GameObject beingHitAudio;
	public GameObject releaseFireAudio;

	//for moving
	private const float LeftBound = 218f;
	private const float RightBound = 245f;
	private const float BottomBound = 22f;
	private const float TopBound = 43f;
	private const float StopTimeUpperBound = 2f;
	private const float StopTimeLowerBound = 0.5f;
	private const float MovingSpeedUpperBound = 5f;
	private const float MovingSpeedLowerBound = 2f;

	//for dying
	public GameObject[] oMonstersWhenDead;

	protected override void Start ()
	{
		base.Start ();

		animator = base.sprite.GetComponent<Animator>();

		StartCoroutine(MainCycle());
	}

	public override void TakeDamage (int damage)
	{
		base.TakeDamage (damage);
		Instantiate(beingHitAudio, transform.position, Quaternion.identity);
	}

	protected override void Die ()
	{
		base.Die();

		foreach (GameObject monster in oMonstersWhenDead)
		{
			monster.transform.position = transform.position;
			monster.SetActive(true);
		}

		Destroy(gameObject);
	}

	IEnumerator MainCycle()
	{
		while (base.health > 0f) 
		{
			//move to a spot
			animator.SetBool("Move", true);
			Vector3 newPos = new Vector3(Random.Range(LeftBound, RightBound), Random.Range(BottomBound, TopBound), 0f);
			float time = Vector3.Distance(newPos, transform.position) / Random.Range(MovingSpeedLowerBound, MovingSpeedUpperBound);
			StartCoroutine(MoveToObjective(transform.position, newPos, time));
			yield return new WaitForSeconds(time);

			//stop for some time
			StopCoroutine("MoveToObjective");
			animator.SetBool("Move", false);
			yield return new WaitForSeconds(Random.Range(StopTimeLowerBound, StopTimeUpperBound));

			//emit fire
			animator.SetTrigger("Shoot");
			yield return new WaitForSeconds(ShootAnimationTime);
			Instantiate(releaseFireAudio, transform.position, Quaternion.identity);
			Instantiate(firePrefabs, shootPosition.transform.position, Quaternion.identity);
			GameObject fire2 = (Instantiate(firePrefabs, shootPosition.transform.position, Quaternion.identity) as GameObject);
			fire2.GetComponent<BossBullet>().side *= -1f;

			//stop for some time
			yield return new WaitForSeconds(Random.Range(StopTimeLowerBound, StopTimeUpperBound));

		}
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
}
