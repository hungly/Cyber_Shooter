using UnityEngine;
using System.Collections;

public class BirdshotProjectile : MonoBehaviour
{
	public float shotForce;
	
	void Start ()
	{
		rigidbody.AddForce (transform.forward * shotForce, ForceMode.Impulse);
	}

	void OnCollisionStay (Collision collision)
	{
		if (rigidbody.velocity.z <= 0 && rigidbody.velocity.z >= -4.5) {
			rigidbody.AddForce (Vector3.back, ForceMode.Impulse);
		}
	}
}
