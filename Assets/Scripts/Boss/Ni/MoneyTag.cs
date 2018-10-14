using UnityEngine;
using System.Collections;

public class MoneyTag : MonoBehaviour 
{
	public GameObject moneyText;

	private TextMesh textMesh;
	
	private int money = 0;
	public int Money {
		get {
			return money;
		}
	}

	private const int MoneyCap = 99;

	void Start()
	{
		textMesh = moneyText.GetComponent<TextMesh>();
	}

	public void AddMoney(int i)
	{
		money += i;
		if (money > MoneyCap) money = MoneyCap;

		textMesh.text = money.ToString();
	}

	public void ClearMoney()
	{
		money = 0;

		textMesh.text = Money.ToString();
	}
}
