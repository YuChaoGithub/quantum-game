using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CooldownIcon : MonoBehaviour {

	public GameObject bar;
	public GameObject backGround;
	public float coolDown;
	public int type;

	private bool isCoolingDown = false;
	private float timer;

	public void StartCountDown()
	{
		if (type != 2) isCoolingDown = true;
		timer = Time.timeSinceLevelLoad;
		backGround.GetComponent<Image>().color = Color.gray;
	}

	void Start()
	{
		Reset();
	}

	void Update () 
	{
		if (!isCoolingDown) return;

		if (Time.timeSinceLevelLoad - timer < coolDown)
		{
			bar.GetComponent<HealthBar>().SetValue(Time.timeSinceLevelLoad - timer);
		}
		else
		{
			Reset();
		}
	}

	void Reset()
	{
		switch (type)
		{
			case 0: backGround.GetComponent<Image>().color = Color.blue; break;
			case 1: backGround.GetComponent<Image>().color = Color.red; break;
			case 2: backGround.GetComponent<Image>().color = Color.green; break;
		}
		if (type != 2) bar.GetComponent<HealthBar>().SetValue(coolDown);
		isCoolingDown = false;
	}

	public void ToggleSkill()
	{
		backGround.GetComponent<Image>().color = Color.yellow;
		bar.GetComponent<HealthBar>().SetValue(0f);
	}
}
