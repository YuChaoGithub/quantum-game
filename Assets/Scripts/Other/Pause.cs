using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Pause : MonoBehaviour 
{
	public GameObject pauseScene;
	public Sprite pauseImage;
	public Sprite resumeImage;

	private bool pausing = false;

	public void ManageClick()
	{
		if (pausing) UnpauseGame();
		else PauseGame ();
	}

	void PauseGame()
	{
		pausing = true;
		Time.timeScale = 0;
		GetComponent<Image>().sprite = resumeImage;
		pauseScene.SetActive(true);
	}

	public void UnpauseGame()
	{
		pausing = false;
		Time.timeScale = 1;
		GetComponent<Image>().sprite = pauseImage;
		pauseScene.SetActive(false);
	}
}
