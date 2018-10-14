using UnityEngine;
using System.Collections;

public class OpenIntroduction : MonoBehaviour 
{
	public GameObject introductionScene;
	public GameObject canvas;

	public void OpenTheScene()
	{
		GameObject scene = (Instantiate(introductionScene, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject);
		scene.transform.SetParent(canvas.transform, false);
	}
}
