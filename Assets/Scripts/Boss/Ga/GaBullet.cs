using UnityEngine;
using System.Collections;

public class GaBullet : MonoBehaviour 
{
    public GameObject redBullet;
    public GameObject yellowBullet;
    public GameObject greenBullet;
    public GameObject blueBullet;
    public float shootingSpeed;
    public float timeUntilExplode;
    public float lifeTime;
	public float damage;

    void Start()
    {
        Invoke("Explode", timeUntilExplode);
        Invoke("Remove", lifeTime);
    }

	void OnCollision2D(Collision2D col)
	{
		if (col.gameObject.tag == "Player") {
			gameObject.GetComponent<Player> ().Hurt (damage);
			Destroy (gameObject);
		}
	}

    void Explode()
    {
		GetComponent<KeepOnRotate> ().enabled = false;
		GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 0f);
		if (yellowBullet != null) {
			yellowBullet.GetComponent<Rigidbody2D> ().isKinematic = false;
			yellowBullet.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, shootingSpeed, 0f);
		}
		if (greenBullet != null) {
			greenBullet.GetComponent<Rigidbody2D> ().isKinematic = false;
			greenBullet.GetComponent<Rigidbody2D> ().velocity = new Vector3 (shootingSpeed, 0f, 0f);
		}
		if (blueBullet != null) {
			blueBullet.GetComponent<Rigidbody2D> ().isKinematic = false;
			blueBullet.GetComponent<Rigidbody2D> ().velocity = new Vector3 (0f, -shootingSpeed, 0f);
		}
		if (redBullet != null) {
			redBullet.GetComponent<Rigidbody2D>().isKinematic = false;
			redBullet.GetComponent<Rigidbody2D>().velocity = new Vector3(-shootingSpeed, 0f, 0f);

		}
    }

    void Remove()
    {
        Destroy(gameObject);
    }

}
