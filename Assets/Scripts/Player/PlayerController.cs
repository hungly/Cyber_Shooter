using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public Rigidbody cameraRigibody;
	public GameObject weaponPoint;
	public GameObject bullet;
	public GUIText test;
	public float smoothTime;
	public float maxPitch;
	public float maxYaw;
	public float pitchCorrection;
	public float stability;
	public float speed;
	private Vector2 screenCorrection;
	private float yPositionCurrentVelocity;
	private float zPositionCurrentVelocity;
	private float xRotationCurrentVelocity;
	private float yRotationCurrentVelocity;

	void Start ()
	{
		screenCorrection = new Vector2 (Screen.width / 2, Screen.height / 2);
	}

	void Update ()
	{
		// if there is a touch event
		if (Input.touchCount > 0) {
			// loop through all touch point
			// this enable multi-touches target shot
			for (int i = 0; i < Input.touchCount; i++) {
				// only activate when touch begin
				if (Input.GetTouch (i).phase == TouchPhase.Began) {
					// get the touch point, reset the point of origin at the middle of the screen
					Vector2 touchPosition = Input.GetTouch (i).position - screenCorrection;

					// calculate the shot angle withe a little correction
					// x from -maxPitch to maxPitch, positive is pitch down, negative is pitch up
					// y from -maxYaw to maxYaw, positive is yaw right, negative is yaw left
					Quaternion angle = Quaternion.Euler (new Vector3 (
						-Mathf.Atan2 (touchPosition.y, screenCorrection.y) * maxPitch - pitchCorrection,
						Mathf.Atan2 (touchPosition.x, screenCorrection.x) * maxYaw,
						0.0f
					));

					// spawn the bullet with prepared information
					Instantiate (bullet, weaponPoint.transform.position, angle);
				}
			}
		}
	}

	void FixedUpdate ()
	{
		StablizePosition ();
		//StablizeRotation ();

		Vector3 predictedUp = Quaternion.AngleAxis(
			rigidbody.angularVelocity.magnitude * Mathf.Rad2Deg * stability / speed,
			rigidbody.angularVelocity
			) * transform.up;
		
		Vector3 torqueVector = Vector3.Cross(predictedUp, cameraRigibody.rotation * Vector3.up);
		rigidbody.AddTorque(torqueVector * speed * speed);
	}
	
	void StablizePosition ()
	{
		transform.position = new Vector3 (
			cameraRigibody.position.x,
			Mathf.SmoothDamp (transform.position.y, 0.0f, ref yPositionCurrentVelocity, smoothTime),
			Mathf.SmoothDamp (transform.position.z, 0.0f, ref zPositionCurrentVelocity, smoothTime)
		);
	}

	void StablizeRotation ()
	{
		rigidbody.rotation = Quaternion.Euler (
			Mathf.SmoothDamp (rigidbody.rotation.eulerAngles.x, 0.0f, ref xRotationCurrentVelocity, smoothTime),
			Mathf.SmoothDamp (rigidbody.rotation.eulerAngles.y, 0.0f, ref yRotationCurrentVelocity, smoothTime),
			cameraRigibody.rotation.eulerAngles.z
		);
	}
}
