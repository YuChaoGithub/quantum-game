using UnityEngine;
using System.Collections;

public class PayingMachine : MonoBehaviour 
{
	public int price;

	public GameObject audio;

	public GameObject moneyText;
	public GameObject moneyTag;

	public GameObject openCollider;
	public GameObject closeCollider;

	private TextMesh textMesh;
	private MoneyTag playerMoney;

	void Start()
	{
		textMesh = moneyText.GetComponent<TextMesh>();
		textMesh.text = price.ToString();

		playerMoney = moneyTag.GetComponent<MoneyTag>();
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			if (playerMoney.Money >= price)
			{
				//animation
				GetComponent<Animator>().SetTrigger("Open");
				Instantiate(audio, transform.position, Quaternion.identity);

				//minus the money of player
				playerMoney.ClearMoney();

				//set collider
				closeCollider.SetActive(false);
				openCollider.SetActive(true);
				GetComponent<BoxCollider2D> ().enabled = false;
			}
		}
	}
}
