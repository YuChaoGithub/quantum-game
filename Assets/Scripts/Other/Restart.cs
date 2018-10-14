using UnityEngine;
using System.Collections;

public class Restart : MonoBehaviour {

	public GameObject loadingImage;

	public void LoadScene()
	{
		Time.timeScale = 1;
		loadingImage.SetActive(true);
		Application.LoadLevel(37);
	}
}
