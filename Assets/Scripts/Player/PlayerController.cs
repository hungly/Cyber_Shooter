using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public Rigidbody cameraRigibody;
	public GameObject weaponPoint;
	public GameObject bullet;
	public GUIText test;
	public float maxPitch;
	public float maxYaw;
	public float pitchCorrection;
	public float stability;
	public float speed;
	public float positionSmoothTime;
	private Vector2 screenCorrection;
	private Vector3 currentPositionVelocity = Vector3.zero;

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
		StabilizePosition ();

		StabilizeRotation ();
	}
	
	void StabilizePosition ()
	{
		//transform.position = new Vector3 (cameraRigibody.position.x, 0.0f, 0.0f);

		rigidbody.position = new Vector3 (
			cameraRigibody.position.x,
			rigidbody.position.y,
			rigidbody.position.z
			);

		rigidbody.velocity = Vector3.SmoothDamp(
			new Vector3(rigidbody.position.x, rigidbody.position.y, rigidbody.position.z),
			new Vector3(rigidbody.position.x, 0.0f,0.0f),
			ref currentPositionVelocity,
			positionSmoothTime
			) * -1;
	}

	void StabilizeRotation ()
	{
		Vector3 predictedUp = Quaternion.AngleAxis (
			rigidbody.angularVelocity.magnitude * Mathf.Rad2Deg * stability / speed,
			rigidbody.angularVelocity
			) * transform.up;
		
		Vector3 torqueVectorUp = Vector3.Cross (predictedUp, cameraRigibody.rotation * Vector3.up);
		
		Vector3 predictedForward = Quaternion.AngleAxis (
			rigidbody.angularVelocity.magnitude * Mathf.Rad2Deg * stability / speed,
			rigidbody.angularVelocity
			) * transform.forward;
		
		Vector3 torqueVectorForward = Vector3.Cross (predictedForward, cameraRigibody.rotation * Vector3.forward);
		
		rigidbody.AddTorque ((torqueVectorUp + torqueVectorForward) * speed * speed);
	}
}
