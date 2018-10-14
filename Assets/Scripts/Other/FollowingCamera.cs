using UnityEngine;
using System.Collections;

public class FollowingCamera : MonoBehaviour 
{
	public float xMargin;
	public float yMargin;
	public float xSmooth;
	public float ySmooth;
	public Vector2 maxXandY;
	public Vector2 minXandY;
	public GameObject playerTransformObj;

	private const float AspectRatio = 16f / 9f;

	private Transform playerTransform;

	void Update()
	{
		Camera cam = GetComponent<Camera> ();
		float screenRatio = (float) Screen.width / (float) Screen.height;
		float scale = screenRatio / AspectRatio;

		if (scale > 1f) {
			Rect pixRect = cam.pixelRect;
			pixRect.width = pixRect.height * AspectRatio;
			pixRect.y = 0f;
			pixRect.x = ((float)Screen.width - pixRect.width) / 2f;
			cam.pixelRect = pixRect;
		} else {
			Rect pixRect = cam.pixelRect;
			pixRect.height = pixRect.width / AspectRatio;
			pixRect.x = 0f;
			pixRect.y = ((float)Screen.height - pixRect.height) / 2f;
			cam.pixelRect = pixRect;
		}
	}

	void Awake()
	{
		//set up the player transform
		playerTransform = playerTransformObj.transform;
	}

	void FixedUpdate()
	{
		float targetX = transform.position.x;
		float targetY = transform.position.y;

		if (xOverMargin()) 
		{
			targetX = Mathf.Lerp(transform.position.x, playerTransform.position.x, xSmooth * Time.deltaTime);
		}

		if (yOverMargin())
		{
			targetY = Mathf.Lerp(transform.position.y, playerTransform.position.y, ySmooth * Time.deltaTime);
		}

		//limited by stage size
		targetX = Mathf.Clamp(targetX, minXandY.x, maxXandY.x);
		targetY = Mathf.Clamp(targetY, minXandY.y, maxXandY.y);

		//set position
		transform.position = new Vector3(targetX,targetY,transform.position.z);
	}

	bool xOverMargin()
	{
		return Mathf.Abs(playerTransform.position.x - transform.position.x) > xMargin;
	}

	bool yOverMargin()
	{
		return Mathf.Abs(playerTransform.position.y - transform.position.y) > yMargin;
	}

}
