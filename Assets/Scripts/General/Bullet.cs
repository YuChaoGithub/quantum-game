using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour 
{
	public float speed;
	public float lifeTime;  //aka distance
	public float side;
	public int damage;

	protected virtual void Start()
	{
		//flip to the correct side
		if (side < 0)
		{
			Vector3 scale = transform.localScale;
			scale.x *= -1;
			transform.localScale = scale;
		}

		GetComponent<Rigidbody2D>().velocity = new Vector2(speed * side, 0f);

		//capture the time when bullet is created
		Invoke("DestroyBullet", lifeTime);
	}
	
	public void DestroyBullet()
	{
		Destroy(gameObject);
	}

}
