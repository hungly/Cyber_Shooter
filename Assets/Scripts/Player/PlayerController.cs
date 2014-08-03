using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

	public float playerHorizontalSpeed;
	public float playerTiltFactor;
	public float levelWidth;
	public float movementSmoothTime;
	private float previousMovement;
	private float currentVelocity;

	// Use this for initialization
	void Start ()
	{
		previousMovement = 0.0f;
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	// FixedUpdate is called once per frame before applying physics
	void FixedUpdate ()
	{
		// get the ammount of device rotation and use it to determine the movement speed
		float horizontalMovement = Input.acceleration.x;

		// apply the movement vector to the player ship
		rigidbody.velocity = new Vector3 (Mathf.SmoothDamp (previousMovement, horizontalMovement, ref currentVelocity, movementSmoothTime), 0.0f, 0.0f) * playerHorizontalSpeed;

		// make sure the player does not go outside of level bound
		rigidbody.position = new Vector3 (Mathf.Clamp (rigidbody.position.x, -levelWidth, levelWidth), rigidbody.position.y, 0.0f);

		// rotate the ship
		rigidbody.rotation = Quaternion.Euler (0.0f, 0.0f, rigidbody.velocity.x * -playerTiltFactor);
		
		previousMovement = horizontalMovement;
	}
}
