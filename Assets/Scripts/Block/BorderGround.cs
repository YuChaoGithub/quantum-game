using UnityEngine;
using System.Collections;

public class BorderGround : MonoBehaviour 
{
	public GameObject restartPosition;

	private const float Damage = 20f;
	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Monster" || col.gameObject.tag == "Bottle")
		{
			Destroy(col.gameObject);
		}
		else if (col.gameObject.tag == "Player")
		{
			col.gameObject.GetComponent<Player>().Hurt(Damage);
			col.gameObject.transform.position = restartPosition.transform.position;
		}
		else if (col.gameObject.tag == "Float Player")
		{
			col.gameObject.GetComponent<FloatPlayer>().Hurt(Damage);
			col.gameObject.transform.position = restartPosition.transform.position;
		}
	}
}
