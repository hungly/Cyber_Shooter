using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	public float cameraMovementSpeed;
	public float cameraRotaionLimit;
	public float horizontalSmoothTime;
	public float levelWidth;
	private float horizontalMovement;
	private float previousHorizontalMovement;
	private float currentHorizontalMovementVelocity;

	void Start ()
	{
		previousHorizontalMovement = 0.0f;
	}

	void FixedUpdate ()
	{
		// get device rotaion value
		horizontalMovement = Mathf.Round (Input.acceleration.x * 1000.0f) / 1000.0f;

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

		previousHorizontalMovement = horizontalMovement;
	}
}
