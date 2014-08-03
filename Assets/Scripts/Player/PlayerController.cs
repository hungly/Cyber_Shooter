using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public Rigidbody cameraRigibody;
	public float smoothTime;
	private float yPositionCurrentVelocity;
	private float zPositionCurrentVelocity;
	private float xRotationCurrentVelocity;
	private float yRotationCurrentVelocity;

	void FixedUpdate ()
	{
		StablizePosition ();
		StablizeRotation ();
	}

	void StablizePosition() {
		rigidbody.position = new Vector3 (
			cameraRigibody.position.x,
			Mathf.SmoothDamp (rigidbody.position.y, 0.0f, ref yPositionCurrentVelocity, smoothTime),
			Mathf.SmoothDamp (rigidbody.position.z, 0.0f, ref zPositionCurrentVelocity, smoothTime)
		);
	}

	void StablizeRotation () {
		rigidbody.rotation = Quaternion.Euler (
			Mathf.SmoothDamp (rigidbody.rotation.eulerAngles.x, 0.0f, ref xRotationCurrentVelocity, smoothTime),
			Mathf.SmoothDamp (rigidbody.rotation.eulerAngles.y, 0.0f, ref yRotationCurrentVelocity, smoothTime),
			cameraRigibody.rotation.eulerAngles.z
			);
	}
}
