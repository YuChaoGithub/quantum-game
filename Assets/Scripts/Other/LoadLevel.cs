using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadLevel : MonoBehaviour 
{
	public GameObject loadingImage;
	public GameObject coloredStartBtn;
	public GameObject bossFrame;
	public GameObject bossImageGameObj;
	public GameObject nameText;
	public Sprite[] bossImages;
	private string[] BossText = {"", "Hydrogen", "Helium", "Lithium", "Beryllium", "Boron", "Carbon", "Nitrogen",
		"Oxygen", "Fluorine", "Neon", "Sodium", "Magnesium", "Aluminum", "Silicon", "Phosphorus", "Sulfur", "Chlorine", "Argon",
		"Potassium", "Calcium", "Scandium", "Titanium", "Vanadium", "Chromium", "Manganese", "Iron", "Cobalt", "Nickel",
		"Copper", "Zinc", "Gallium", "Germanium", "Arsenic", "Selenium", "Bromine", "Krypton"};

	private int scene_index;

	public void LoadScene()
	{
		loadingImage.SetActive(true);
		Application.LoadLevel(scene_index);
	}

	public void OnClickLevelPicker(int scene)
	{
		bossImageGameObj.GetComponent<Image>().sprite = bossImages[scene];
		nameText.GetComponent<Text>().text = BossText[scene];
		if (!bossFrame.activeSelf) 
		{
			coloredStartBtn.SetActive(true);
			bossFrame.SetActive(true);
		}
		scene_index = scene;
		coloredStartBtn.SetActive(true);
	}
}
