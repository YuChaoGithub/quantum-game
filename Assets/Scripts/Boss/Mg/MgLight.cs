using UnityEngine;
using System.Collections;

public class MgLight : MonoBehaviour
{
	public GameObject flashLight;
	public GameObject mask;

	public GameObject countDownAudio;
	public GameObject focusAudio;
	public GameObject shotAudio;

	public float interval = 3f;

	private SpriteRenderer maskRenderer;

	private const float CountdownTime = 3.5f;
	private const float FlashDelay = 0.05f;
	private const float FocusSoundDelay = 0.25f;
	private const float FlashDuration = 0.1f;
	private const float SoundDelay = 1f;

	void Start()
	{
		maskRenderer = mask.GetComponent<SpriteRenderer>();
		StartCoroutine(MainCycle());
	}

	IEnumerator MainCycle()
	{
		while (true)
		{
			GetComponent<Animator>().SetBool("Light On", true);
			yield return new WaitForSeconds(SoundDelay);
			Instantiate(countDownAudio, transform.position, Quaternion.identity);

			//counting down
			yield return new WaitForSeconds(CountdownTime - SoundDelay);
			Instantiate(focusAudio, transform.position, Quaternion.identity);

			//flash
			yield return new WaitForSeconds(FocusSoundDelay);
			flashLight.SetActive(true);
			Instantiate(shotAudio, transform.position, Quaternion.identity);

			//gradually adjust the alpha of the mask
			float i = 0f;
			while (i < 1f)
			{
				i += Time.deltaTime / FlashDelay;
				Color newCol = maskRenderer.color;
				newCol.a = i;
				maskRenderer.color = newCol;
				yield return null;
			}

			GetComponent<Animator>().SetBool("Light On", false);
			flashLight.SetActive(false);

			//duration of flash
			yield return new WaitForSeconds(FlashDuration);

			//turn off the mask
			i = 0f;
			while (i < 1f)
			{
				i += Time.deltaTime / FlashDelay;
				Color newCol = maskRenderer.color;
				newCol.a = (1f - i);
				maskRenderer.color = newCol;
				yield return null;
			}

			//interval
			yield return new WaitForSeconds(interval);

		}
	}
}
