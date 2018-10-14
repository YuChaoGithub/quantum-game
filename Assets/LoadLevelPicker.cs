using UnityEngine;
using System.Collections;

public class LoadLevelPicker : MonoBehaviour
{
	public float loadSec;

	void Start()
	{
		Invoke ("LoadTheLevel", loadSec);
	}

	void LoadTheLevel()
	{
		Application.LoadLevel (37);
	}
}
