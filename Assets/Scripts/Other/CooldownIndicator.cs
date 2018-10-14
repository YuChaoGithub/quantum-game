using UnityEngine;
using System.Collections;

public class CooldownIndicator : MonoBehaviour 
{

	public GameObject electronKnife; //0
	public GameObject electronBomb;  //1

	public GameObject protonWave;    //2
	public GameObject protonHide;       //3

	public GameObject neutronBullet; //4

	private int currentType = 0;

	void Reset()
	{
		electronKnife.SetActive(false);
		electronBomb.SetActive(false);
		protonWave.SetActive(false);
		protonHide.SetActive(false);
		neutronBullet.SetActive(false);
	}

	//Don't know why, but I have to put in this function in order to make the electron icons work initially.
	void Start()
	{
		Reset();
		electronKnife.SetActive(true);
		electronBomb.SetActive(true);
	}

	public void SwitchTo(int i)
	{
		if (currentType == 0)
		{
			electronKnife.SetActive(false);
			electronBomb.SetActive(false);
			protonWave.SetActive(true);
			protonHide.SetActive(true);
			currentType = 1;
		}
		else if (currentType == 1)
		{
			protonWave.SetActive(false);
			protonHide.SetActive(false);

			if (i == 0)
			{
				electronKnife.SetActive(true);
				electronBomb.SetActive(true);
				currentType = 0;
			}
			else{
				neutronBullet.SetActive(true);
				currentType = 2;
			}
		}
		else if (currentType == 2)
		{
			neutronBullet.SetActive(false);
			protonWave.SetActive(true);
			protonHide.SetActive(true);
			currentType = 1;
		}
	}
	
}
