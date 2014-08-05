using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	// camera maximum movement speed
	public float cameraMovementSpeed;
	// camera maximum rotaion angle
	public float cameraRotaionLimit;
	// time factor for smooth camera movement
	public float horizontalSmoothTime;
	// level size, determined how much the camera will move to sides
	public float levelWidth;
	// horizontal movement for the camera
	private float horizontalMovement;
	// the previous value of horizontal movement
	private float previousHorizontalMovement;
	// current velocity correction
	private float currentHorizontalMovementVelocity;

	void Start ()
	{
		previousHorizontalMovement = 0.0f;
	}

	void FixedUpdate ()
	{
		// get device rotaion value remove some minor number in attemp to remove screen vibration
		horizontalMovement = Mathf.Round (Input.acceleration.x * 100.0f) / 100.0f;

		// smooth the rotaion
		float actualHorizontalMovement = Mathf.SmoothDamp (previousHorizontalMovement, horizontalMovement, ref currentHorizontalMovementVelocity, horizontalSmoothTime);

		// match camera rotaion with device rotation
		rigidbody.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, actualHorizontalMovement) * -cameraRotaionLimit);

		// move camera with speed adjust by the rotation angle
		rigidbody.velocity = new Vector3 (actualHorizontalMovement, rigidbody.position.y, rigidbody.position.z) * cameraMovementSpeed;

		// make sure the camera does not go to far to the left or right
		rigidbody.position = new Vector3 (
			Mathf.Clamp (rigidbody.position.x, -levelWidth, levelWidth),
			rigidbody.position.y,
			rigidbody.position.z
		);

		// store new value
		previousHorizontalMovement = horizontalMovement;
	}
}
