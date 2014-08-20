using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	// projectile types
	public GameObject bullet;
	public GameObject ball;
	public GameObject missile;
	public GameObject shotgun;
	public GameObject laser;
	// weapon point reference
	public GameObject weaponPoint;
	// camera position
	public Rigidbody cameraRigibody;
	// weapon variables
	public float maxPitch;
	public float maxYaw;
	public float pitchCorrection;
	// stabilizer values
	public float stability;
	public float speed;
	public float xPositionSmoothTime;
	public float positionSmoothTime;
	// some projectile values
	public float shotgunSpreadFactor;
	// screen size for correction
	private Vector2 screenCorrection;
	// projectiles array
	private GameObject[] projectiles;
	// position movement correction velocity
	private float currentXPositionVelocity;
	private float currentYPositionVelocity;
	private float currentZPositionVelocity;

	void Start ()
	{
		// get the screen size to use for weapon correction
		screenCorrection = new Vector2 (Screen.width / 2, Screen.height / 2);
		// build projectiles array
		projectiles = new GameObject[]{ball, missile, shotgun, laser};
	}

	void Update ()
	{
		// if there is a touch event
		if (Input.touchCount > 0) {
			// loop through all touch point
			// this enable multi-touches for shooting
			for (int i = 0; i < Input.touchCount; i++) {
				// only activate when the touch action begin
				if (Input.GetTouch (i).phase == TouchPhase.Began) {
					// get the touch point, reset the point of origin at the middle of the screen
					Vector2 touchPosition = Input.GetTouch (i).position - screenCorrection;

					Vector3 bulletPosition = Vector3.zero;
					Vector3 weaponMuzzleCorrection = new Vector3 (0.0f, -0.25f, 1.5f);

					Quaternion angle = Quaternion.identity;

					if (bullet.tag == "ShotgunProjectile") {
						for (int j = 0; j < 5; j++) {
							switch (j) {
							case 1:
								bulletPosition.x = -0.3f;
								bulletPosition.y = 0.3f;
								break;
								
							case 2:
								bulletPosition.x = 0.3f;
								bulletPosition.y = 0.3f;
								break;
								
							case 3:
								bulletPosition.x = 0.3f;
								bulletPosition.y = -0.3f;
								break;
								
							case 4:
								bulletPosition.x = -0.3f;
								bulletPosition.y = -0.3f;
								break;
							}

							Quaternion angleVary = Random.rotation;

							// calculate the shot angle with a little correction
							// x from -maxPitch to maxPitch, positive is pitch down, negative is pitch up
							// y from -maxYaw to maxYaw, positive is yaw right, negative is yaw left
							angle = Quaternion.Euler (new Vector3 (
								-Mathf.Atan2 (touchPosition.y, screenCorrection.y) * maxPitch - pitchCorrection + angleVary.x * shotgunSpreadFactor,
								Mathf.Atan2 (touchPosition.x, screenCorrection.x) * maxYaw + angleVary.y * shotgunSpreadFactor,
								rigidbody.rotation.eulerAngles.z
							));
							
							// spawn the bullet with prepared information
							Instantiate (bullet, weaponPoint.transform.position + bulletPosition + weaponMuzzleCorrection, angle);
						}
					} else {
						// calculate the shot angle with a little correction
						// x from -maxPitch to maxPitch, positive is pitch down, negative is pitch up
						// y from -maxYaw to maxYaw, positive is yaw right, negative is yaw left
						angle = Quaternion.Euler (new Vector3 (
							-Mathf.Atan2 (touchPosition.y, screenCorrection.y) * maxPitch - pitchCorrection,
							Mathf.Atan2 (touchPosition.x, screenCorrection.x) * maxYaw,
							rigidbody.rotation.eulerAngles.z
						));

						// spawn the bullet with prepared information
						Instantiate (bullet, weaponPoint.transform.position + weaponMuzzleCorrection, angle);
					}

					weaponPoint.transform.rotation = angle;
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

		//rigidbody.position = new Vector3 (
		//	cameraRigibody.position.x,
		//	rigidbody.position.y,
		//	rigidbody.position.z
		//);

		float xPosition = Mathf.SmoothDamp (rigidbody.position.x, cameraRigibody.position.x, ref currentXPositionVelocity, xPositionSmoothTime);
		float yPosition = Mathf.SmoothDamp (rigidbody.position.y, 0.0f, ref currentYPositionVelocity, positionSmoothTime);
		float zPosition = Mathf.SmoothDamp (rigidbody.position.z, 0.0f, ref currentZPositionVelocity, positionSmoothTime);

		rigidbody.position = new Vector3 (xPosition, yPosition, zPosition);
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
