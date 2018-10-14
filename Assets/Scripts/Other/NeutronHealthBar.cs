using UnityEngine;
using System.Collections;

public class NeutronHealthBar : HealthBar
{
	void OnEnable()
	{
		bar.transform.localScale = new Vector3(0.99f, 0.9f, 1f);
	}

	protected override void Update()
	{
		base.Update();

		if (base.bar.transform.localScale.x < 0.01) gameObject.SetActive(false);
	}
}
