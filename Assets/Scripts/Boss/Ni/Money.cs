using UnityEngine;
using System.Collections;

public class Money : MonoBehaviour 
{
	public int value;
	public GameObject moneyTag;
	public GameObject audio;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			moneyTag.GetComponent<MoneyTag>().AddMoney(value);
			Instantiate(audio, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
