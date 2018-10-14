using UnityEngine;
using System.Collections;

public class Chlorine : Boss
{
	public GameObject bossBullet1;
	public GameObject bossBullet2;
	public GameObject bossBullet3;
	public GameObject shootPos;

	public GameObject beingHitAudio;
	public GameObject releaseBulletAudio;

	private const float DieAnimationTime = 1f;
	private Vector3 upperBound = new Vector3(240f, 78f, 0f);
	private Vector3 lowerBound = new Vector3(240f, 53f, 0f);
	private const float ShootInterval = 1f;
	private const float MovementSpeed = 3f;
	private const int MaxShootTime = 3;
	private const float BulletVelocityY = 200f;

	protected override void Start ()
	{
		base.Start ();

		StartCoroutine(Movement());
	}

	public override void TakeDamage (int damage)
	{
		base.TakeDamage (damage);

		Instantiate(beingHitAudio, transform.position, Quaternion.identity);
	}

	IEnumerator Movement()
	{
		while (base.health > 0f) 
		{
			//move to a spot
			Vector3 newPos = new Vector3(upperBound.x, Random.Range(lowerBound.y, upperBound.y), 0f);
			float time = Vector3.Distance(transform.position, newPos) / MovementSpeed;
			StartCoroutine(MoveToObjective(transform.position, newPos, time));
			yield return new WaitForSeconds(time);
			StopCoroutine("MoveToObjective");

			//shoot
			int shootTimes = Random.Range(1, MaxShootTime);
			for (int i = 0; i < shootTimes; i++) {
				yield return new WaitForSeconds(ShootInterval);
				Instantiate(bossBullet1, shootPos.transform.position, Quaternion.identity);
				GameObject bul2 = (Instantiate(bossBullet2, shootPos.transform.position, Quaternion.identity) as GameObject);
				bul2.GetComponent<Rigidbody2D>().AddForce(new Vector3(0f, BulletVelocityY, 0f));
				GameObject bul3 = (Instantiate(bossBullet3, shootPos.transform.position, Quaternion.identity) as GameObject);
				bul3.GetComponent<Rigidbody2D>().AddForce(new Vector3(0f, -BulletVelocityY, 0f));
				Instantiate(releaseBulletAudio, transform.position, Quaternion.identity);
			}
			yield return new WaitForSeconds(ShootInterval);
		}
	}

	protected override void Die ()
	{
		base.Die ();

		base.bottle.transform.position = transform.position;
		base.sprite.GetComponent<Animator>().SetTrigger("Die");
		Invoke("Remove", DieAnimationTime);
	}

	void Remove()
	{
		Destroy(gameObject);
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
