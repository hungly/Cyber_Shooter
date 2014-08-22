using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour
{
	void OnCollisionEnter (Collision collision)
	{
		if (collision.gameObject.tag == "BallProjectile" ||
			collision.gameObject.tag == "ShotgunProjectile" ||
			collision.gameObject.tag == "LaserProjectile" ||
			collision.gameObject.tag == "Player") {
			Destroy (gameObject, 1);
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "BallProjectile" ||
			other.tag == "ShotgunProjectile" ||
			other.tag == "LaserProjectile" ||
			other.tag == "Player") {
			Destroy (gameObject, 1);
		}
	}
}
