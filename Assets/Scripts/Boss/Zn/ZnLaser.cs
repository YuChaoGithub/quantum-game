using UnityEngine;
using System.Collections;

public class ZnLaser : MonoBehaviour 
{
	public GameObject upperLaserHead;
	public GameObject lowerLaserHead;

	public GameObject[] laserBeams;

	public float[] offTimeRange;
	public float prepareTime;
	public float[] blinkTimeRange;
	public float[] laserBeamBlinkRange;
	public float[] laserBeamIntervalRange;

	private Animator upperLaserAnim;
	private Animator lowerLaserAnim;

	void Start()
	{
		//init animator
		upperLaserAnim = upperLaserHead.GetComponent<Animator>();
		lowerLaserAnim = lowerLaserHead.GetComponent<Animator>();

		StartCoroutine(LaserCycle());
	}

	IEnumerator LaserCycle()
	{
		while (true)
		{
			//prepare
			upperLaserAnim.SetTrigger("Prepare");
			lowerLaserAnim.SetTrigger("Prepare");
			yield return new WaitForSeconds(prepareTime);

			//activate
			upperLaserAnim.SetTrigger("Activate");
			lowerLaserAnim.SetTrigger("Activate");
			StartCoroutine("LaserBeamBlink1");
			StartCoroutine("LaserBeamBlink2");
			StartCoroutine("LaserBeamBlink3");
			yield return new WaitForSeconds(Random.Range(blinkTimeRange[0],blinkTimeRange[1]));

			//off 
			upperLaserAnim.SetTrigger("Off");
			lowerLaserAnim.SetTrigger("Off");
			StopCoroutine("LaserBeamBlink1");
			StopCoroutine("LaserBeamBlink2");
			StopCoroutine("LaserBeamBlink3");
			foreach (GameObject laser in laserBeams)
				laser.SetActive(false);
			
			yield return new WaitForSeconds(Random.Range(offTimeRange[0], offTimeRange[1]));
			
		}
	}

	//The below code is so ugly, may change later if think of a better implementation

	IEnumerator LaserBeamBlink1()
	{
		while (true)
		{
			laserBeams[0].SetActive(true);
			yield return new WaitForSeconds(Random.Range(laserBeamBlinkRange[0],laserBeamBlinkRange[1]));
			laserBeams[0].SetActive(false);
			yield return new WaitForSeconds(Random.Range(laserBeamIntervalRange[0],laserBeamIntervalRange[1]));
		}
	}

	IEnumerator LaserBeamBlink2()
	{
		while (true)
		{
			laserBeams[1].SetActive(true);
			yield return new WaitForSeconds(Random.Range(laserBeamBlinkRange[0],laserBeamBlinkRange[1]));
			laserBeams[1].SetActive(false);
			yield return new WaitForSeconds(Random.Range(laserBeamIntervalRange[0],laserBeamIntervalRange[1]));
		}
	}

	IEnumerator LaserBeamBlink3()
	{
		while (true)
		{
			laserBeams[2].SetActive(true);
			yield return new WaitForSeconds(Random.Range(laserBeamBlinkRange[0],laserBeamBlinkRange[1]));
			laserBeams[2].SetActive(false);
			yield return new WaitForSeconds(Random.Range(laserBeamIntervalRange[0],laserBeamIntervalRange[1]));
		}
	}
}

