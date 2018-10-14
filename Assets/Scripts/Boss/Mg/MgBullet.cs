using UnityEngine;
using System.Collections;

public class MgBullet : BossBullet
{

	protected override void Start()
	{
		Invoke("Remove", base.lifeTime);
	}

	void Remove()
	{
		Destroy(gameObject);
	}

	public void SetObjective(Vector3 startPos, Vector3 endPos)
	{
		float distance = Vector3.Distance(startPos, endPos);
		float xDir = (endPos.x - startPos.x) / distance;
		float yDir = (endPos.y - startPos.y) / distance;

		GetComponent<Rigidbody2D>().velocity = new Vector3(base.speed * xDir, base.speed * yDir);
	}


}