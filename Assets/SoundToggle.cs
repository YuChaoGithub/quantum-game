using UnityEngine;
using System.Collections;

public class SoundToggle : MonoBehaviour {
	public GameObject otherButton;

	public void ToggleSound(bool soundOn)
	{
		AudioListener.volume = (soundOn ? 1f : 0f);
		gameObject.SetActive(false);
		otherButton.SetActive(true);
	}
}
