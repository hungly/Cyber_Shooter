using UnityEngine;
using System.Collections;

public class EnableGravityController : MonoBehaviour
{

	void OnCollisionEnter (Collision collision)
	{
		if (collision.gameObject.tag == "BallProjectile" || collision.gameObject.tag == "ShotgunProjectile" ||
			collision.gameObject.tag == "LaserProjectile" || collision.gameObject.tag == "MissileProjectile" ||
			collision.gameObject.tag == "Player") {
			if (rigidbody != null) {
				rigidbody.isKinematic = false;
				rigidbody.useGravity = true;
			}
			Debug.Log ("Collide");
		}
	}
	
	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "BallProjectile" || other.tag == "ShotgunProjectile" ||
			other.tag == "LaserProjectile" || other.tag == "MissileProjectile" ||
			other.tag == "Player") {
			if (rigidbody != null) {
				rigidbody.isKinematic = false;
				rigidbody.useGravity = true;
			}
			Debug.Log ("Trigger");
		}
	}
}
