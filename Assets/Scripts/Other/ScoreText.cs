using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour 
{
	private Text txtCom;

	private int score;
	public int Score
	{
		get
		{
			return score;
		}
		set
		{
			score = value;
			if (score < 0) score = 0;

			txtCom.text = score.ToString();
		}
	}

	void Start()
	{
		txtCom = GetComponent<Text>();
		Score = 0;
	}
}
