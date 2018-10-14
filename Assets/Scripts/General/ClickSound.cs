using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ClickSound : MonoBehaviour 
{
	public AudioClip audio;

	private Button button { get{ return GetComponent<Button>();}}
	private AudioSource audioSource { get{ return GetComponent<AudioSource>();}}

	void Start()
	{
		gameObject.AddComponent<AudioSource>();
		audioSource.clip = audio;
		audioSource.volume = 0.5f;
		audioSource.playOnAwake = false;
		button.onClick.AddListener(() => PlaySound());
	}

	void PlaySound()
	{
		audioSource.PlayOneShot(audio);
	}
}
