using UnityEngine;
using System.Collections;

public class BossTrigger : MonoBehaviour 
{
	public GameObject boss;

	protected virtual void OnTriggerEnter2D(Collider2D col) {}
}
