using UnityEngine;
using System.Collections;

public class FollowBoss : MonoBehaviour 
{
	public GameObject boss;

	void Start()
	{
		transform.position = boss.transform.position;
	}
}
