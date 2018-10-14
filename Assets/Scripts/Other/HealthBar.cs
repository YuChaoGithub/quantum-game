using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour 
{

	public float fullValue;
	public GameObject bar;

	private float value;

	void OnEnable()
	{
		value = fullValue;
	}

	protected virtual void Update()
	{
		Vector3 scale = bar.transform.localScale;
		scale.x = value/fullValue;

		if (scale.x <= 0) scale.x = 0f;

		bar.transform.localScale = scale;

	}

	public void AddValue(float f)
	{
		value += f;
	}

	public void MinusValue(float f)
	{
		value -= f;
	}

 	public void SetValue(float f)
	{
		value = f;
	}
}
