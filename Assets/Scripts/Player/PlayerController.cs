﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	// projectile types
	public GameObject bullet;
	public GameObject ball;
	public GameObject missile;
	public GameObject shotgun;
	public GameObject laser;
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
	private Vector3 spawnPoint;
	// projectiles array
	private GameObject[] projectiles;
	// position movement correction velocity
	private float currentXPositionVelocity;
	private float currentYPositionVelocity;
	private float currentZPositionVelocity;
	private float defaultVolume;

	void Start ()
	{
		Social.ReportProgress ("CgkI68ebh5kcEAIQAQ", 100.0f, (bool successs) => {});
		// get the screen size to use for weapon correction
		screenCorrection = new Vector2 (Screen.width / 2, Screen.height / 2);
		// build projectiles array
		projectiles = new GameObject[]{ball,  shotgun,missile, laser};
		defaultVolume = audio.volume;
	}

	void Update ()
	{
		audio.volume = defaultVolume * (PlayerPrefs.HasKey ("sfxvol") ? PlayerPrefs.GetFloat ("sfxvol") : 0.5f);

		pitchCorrection = bullet.tag == "BallProjectile" ? 0 : pitchCorrection;
		// if there is a touch event
		bool isGamePaused = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().IsGamePaused ();

		if (Input.touchCount > 0 && !isGamePaused) {
			int projectileType = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().GetItemAchieved ();

			// loop through all touch point
			// this enable multi-touches for shooting
			for (int i = 0; i < Input.touchCount; i++) {
				// spawn point posittion
				spawnPoint = new Vector3 (transform.position.x, transform.position.y, transform.position.z + 1);

				if (Input.GetTouch (i).position.x > Screen.width - GameObject.FindWithTag ("PauseButton").GetComponent<GameMenuController> ().pauseButtonStyle.fixedWidth && 
					Input.GetTouch (i).position.y > Screen.height - GameObject.FindWithTag ("PauseButton").GetComponent<GameMenuController> ().pauseButtonStyle.fixedHeight) {
					continue;
				}

				// only activate when the touch action begin
				if (Input.GetTouch (i).phase == TouchPhase.Began) {
					// get the touch point, reset the point of origin at the middle of the screen
					Vector2 touchPosition = Input.GetTouch (i).position - screenCorrection;

					Vector3 bulletPosition = Vector3.zero;

					Quaternion angle = Quaternion.identity;

					if (projectiles [projectileType].tag == "ShotgunProjectile") {
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
							Instantiate (projectiles [projectileType], spawnPoint + bulletPosition, angle);
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
						Instantiate (projectiles [projectileType], spawnPoint, angle);

					}
					GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().bulletShot (1);

					audio.clip = (AudioClip)Resources.Load ("projectile-" + projectileType);

					audio.Play ();
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

	void OnTriggerEnter (Collider other)
	{
		if (other.tag != "BallProjectile" && other.tag != "ShotgunProjectile"
			&& other.tag != "LaserProjectile" && other.tag != "MissileProjectile"
			&& other.tag != "Trigger" && other.tag != "PopupBlock" && other.tag != "Boundary") {
			GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().bulletShot (10);

			GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().resetItemShotConsecutivelyWithoutBeingHit ();

			audio.Play ();
		}
	}

	void OnCollisionEnter (Collision collision)
	{
		if (collision.gameObject.tag != "BallProjectile" && collision.gameObject.tag != "ShotgunProjectile"
			&& collision.gameObject.tag != "LaserProjectile" && collision.gameObject.tag != "MissileProjectile"
			&& collision.gameObject.tag != "Trigger" && collision.gameObject.tag != "PopupBlock") {
			GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().bulletShot (10);
			
			GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().resetItemShotConsecutivelyWithoutBeingHit ();

			audio.clip = (AudioClip)Resources.Load ("ship-hit");
			audio.Play ();
		}
	}

}
