using UnityEngine;
using System.Collections;

public class RegisterToGarbageCollection : MonoBehaviour 
{
	void Start()
	{
		DeleteB.boronList.Add(gameObject);
	}
}
