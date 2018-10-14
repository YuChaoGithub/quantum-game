using UnityEngine;
using System.Collections;

public class Helium : Boss
{
	public GameObject bullet;
	public GameObject shootPos;
	public GameObject dieShootPos;

	//AUDIO
	public GameObject tossAudio;
	public GameObject beingHit;
	
	private float side = 1f; //to left: 1, to right: -1

	private Vector3 firstStopPlace = new Vector3(30f, 114f);
	private const float FirstStopTime = 2.5f;
	private const float ToLeftOrRightWallTime = 5f;

	private Vector3 rightSpot = new Vector3(49f, 113.5f);
	private Vector3 leftSpot = new Vector3(15.5f, 113f);
	private const float UpperBoundY = 118f;
	private const float LowerBoundY = 113f;
	private const float TimeUpperBound = 2f;
	private const float TimeLowerBound = 1f;

	private const float TimeToBulletAppear = 0.7f;
	private const float BulletTossAnimTime = 0.9f;
	private const float BulletForceScale = 10f;
	private const float BulletForceBase = 5f;
	private const float BulletChangeDirTime = 0.3f;
	private const float ReflectionScale = 1.2f;
	
	private float MoveToOppositeChance = 0.35f;
	private const float MoveUpOrDownChance = 0.25f;
	//toss chance = 1 - MoveToOppositeChance - MoveUpOrDownChance

	private const float ToOppositeSpotTime = 6f;

	private Vector3 diePosition = new Vector3(-5f, 114f);
	private const float DieMovingTime = 15f;
	private int dieBomb = 10;

	public void StartMovement()
	{
		StartCoroutine(MovementLoop());
	}

	public override void TakeDamage (int damage)
	{
		base.TakeDamage (damage);

		Instantiate(beingHit, transform.position, Quaternion.identity);
	}

	IEnumerator MovementLoop()
	{
		//rise up to the middle
		StartCoroutine(MoveTo(transform.position, firstStopPlace, FirstStopTime));
		yield return new WaitForSeconds(FirstStopTime);

		//see to go left or right
		if (Random.value > 0.5)
		{
			//right
			side *= -1;
		}
		StartCoroutine(MoveTo(transform.position, (side > 0? leftSpot : rightSpot), ToLeftOrRightWallTime));
		yield return new WaitForSeconds(ToLeftOrRightWallTime);

		while (base.health > 0f)
		{
			float randomNum = Random.value;

			//move up or down
			if (randomNum < MoveUpOrDownChance)
			{
				float time = Random.Range(TimeLowerBound, TimeUpperBound);
				Vector3 objective = new Vector3(transform.position.x, Random.Range(LowerBoundY, UpperBoundY));

				StartCoroutine(MoveTo(transform.position, objective, time));
				yield return new WaitForSeconds(time);
			}
			//move to the opposite spot
			else if (randomNum < MoveUpOrDownChance + MoveToOppositeChance)
			{
				if (MoveToOppositeChance > 0.1f) MoveToOppositeChance -= 0.05f; //reduce the chance

				StartCoroutine(MoveTo(transform.position, (side > 0? rightSpot : leftSpot), ToOppositeSpotTime));
				side *= -1;
				yield return new WaitForSeconds(ToOppositeSpotTime);
			}
			//toss bullet
			else
			{
				base.sprite.GetComponent<Animator>().SetTrigger("Toss");
				Invoke("TossBullet", TimeToBulletAppear);
				yield return new WaitForSeconds(BulletTossAnimTime);
			}
		}

	}

	void TossBullet()
	{
		Instantiate(tossAudio, transform.position, Quaternion.identity);

		GameObject bul = (Instantiate(bullet, shootPos.transform.position, Quaternion.identity)) as GameObject;

		float randomX = Random.value * BulletForceScale + BulletForceBase;
		float randomY = Random.value * BulletForceScale + BulletForceBase;


		if (side == -1)
		{
			bul.GetComponent<Rigidbody2D>().velocity = new Vector2(BulletForceBase + BulletForceScale, BulletForceBase + BulletForceScale);
			StartCoroutine(ChangeBulletDir(bul, randomX, randomY));
		}
		else
		{
			bul.GetComponent<Rigidbody2D>().velocity = new Vector2(randomX, randomY);
		}
	}

	IEnumerator ChangeBulletDir(GameObject bul, float x, float y)
	{
		yield return new WaitForSeconds(BulletChangeDirTime);
		if (bul != null) bul.GetComponent<Rigidbody2D>().velocity = new Vector2(-x * ReflectionScale, y);

		yield return null;
	}

	IEnumerator MoveTo(Vector3 init, Vector3 final, float time)
	{
		float i = 0f;

		while (i <= 1f)
		{
			i += Time.deltaTime / time;
			Vector3 newPos = Vector3.Lerp(init, final, i);
			transform.position = newPos;

			yield return null;
		}
	}

	protected override void Die()
	{
		StopAllCoroutines();

		base.Die();
		base.bottle.transform.parent = null;
		base.sprite.GetComponent<Animator>().SetTrigger("Die");

		//Die movement
		StartCoroutine(MoveTo(transform.position, diePosition, DieMovingTime));

		//drop bombs
		InvokeRepeating("DropBomb", 0f, DieMovingTime/dieBomb);
	}

	void DropBomb()
	{
		if (dieBomb > 0)
		{
			Instantiate(bullet, dieShootPos.transform.position, Quaternion.identity);
		}
		else
		{
			CancelInvoke("DropBomb");
		}
	}
}
