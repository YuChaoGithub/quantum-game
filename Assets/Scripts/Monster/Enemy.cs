using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
	public float speed;

	public GameObject hitAudio;

	public float damage;

	public int side;

	protected virtual void Start()
	{
		side = -1;
	}

	protected virtual void Update()
	{
		transform.Translate(new Vector3(side * speed * Time.deltaTime, 0, 0));
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Wall" || col.gameObject.tag == "Monster") ChangeSide();
	}

	public virtual void ChangeSide()
	{
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;

		side *= -1;
	}

	public virtual void BeingHit()
	{
		Instantiate(hitAudio, transform.position, Quaternion.identity);
	}
}
