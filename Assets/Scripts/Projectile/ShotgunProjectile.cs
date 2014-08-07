using UnityEngine;
using System.Collections;

public class ShotgunProjectile : MonoBehaviour
{
	public float shotForce;

	void Start ()
	{
		rigidbody.AddForce (transform.forward * shotForce, ForceMode.Impulse);
	}
	
	void OnCollisionStay (Collision collision)
	{
		if (-5.0f <= rigidbody.velocity.z && rigidbody.velocity.z <= 0.5f) {
			rigidbody.AddForce (Vector3.back * 10, ForceMode.Acceleration);
		}
	}
}
