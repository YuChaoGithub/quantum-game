using UnityEngine;
using System.Collections;

public class MnWall : MonoBehaviour 
{
	public Sprite[] sprites;

	public GameObject[] debrisPos;
	public GameObject[] debris;

	private int spriteNum;
	private int currIndex = 0;

	private SpriteRenderer renderer;
	private Color originalColor;

	private const int DebrisChoose = 6;

	private const float Force = 50f;

	void Start()
	{
		spriteNum = sprites.Length;
		renderer = GetComponent<SpriteRenderer>();
		originalColor = renderer.color;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player Bullet" || col.gameObject.tag == "Player Knife")
		{
			BeingHit();
			Destroy(col.gameObject);
		}
	}

	void BeingHit()
	{
		currIndex++;

		SpawnDebris();

		if (currIndex == spriteNum)
		{
			Destroy(gameObject);
		}
		else
		{
			renderer.sprite = sprites[currIndex];
		}
	}

	void SpawnDebris()
	{
		for (int i = 0; i < DebrisChoose; i++)
		{
			int randPos = Random.Range(0, debrisPos.Length);

			GameObject currObj = (Instantiate(debris[currIndex-1], debrisPos[randPos].transform.position, Quaternion.identity)) as GameObject;

			currObj.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-Force, Force), 0f));
		}
	}
}
