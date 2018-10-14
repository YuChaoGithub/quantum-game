using UnityEngine;
using System.Collections;

public class InstructionUI : MonoBehaviour 
{
    public GameObject instruction1;
    public GameObject instruction2;

    private AudioSource audioSource { get { return GetComponent<AudioSource>();} }

    public void CloseInstruction()
    {
		audioSource.Play();
        Destroy(gameObject);
    }

    public void nextPage()
    {
		audioSource.Play();
        instruction1.SetActive(false);
        instruction2.SetActive(true);
    }

    public void prevPage()
    {
		audioSource.Play();
        instruction1.SetActive(true);
        instruction2.SetActive(false);
    }
}
