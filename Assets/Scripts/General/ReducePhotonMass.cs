using UnityEngine;
using System.Collections;

public class ReducePhotonMass : MonoBehaviour 
{
	public float adjustedMass;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Photon")
			col.GetComponent<Rigidbody2D>().mass = adjustedMass;
	}
}
