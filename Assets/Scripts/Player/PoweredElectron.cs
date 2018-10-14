using UnityEngine;
using System.Collections;

public class PoweredElectron : MonoBehaviour 
{
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player" && col.GetComponent<Player>().particleType == 1 && !col.GetComponent<Player>().isTransforming)
		{
			col.GetComponent<Proton>().SwitchToNeutron();
			Destroy(gameObject);
		}
	}
}
