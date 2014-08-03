using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;
	public float cameraRotaionTime;
	private Vector3 offset;
	private float currentVelocity;

	// Use this for initialization
	void Start () {
		// get the current camera position
		offset = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		// match the cammera positon position with the player ship
		transform.position = player.rigidbody.position + offset;
		transform.rotation = Quaternion.Euler (player.rigidbody.rotation.eulerAngles.x, player.rigidbody.rotation.eulerAngles.y,
		                                                   Mathf.SmoothDampAngle(transform.rotation.eulerAngles.z, player.rigidbody.rotation.eulerAngles.z, ref currentVelocity, cameraRotaionTime));
	}
}
