using UnityEngine;
using System.Collections;

public class DisableLaserController : MonoBehaviour
{

	void OnCollisionEnter (Collision collision)
	{
		if (collision.gameObject.tag == "BallProjectile" || collision.gameObject.tag == "ShotgunProjectile" ||
			collision.gameObject.tag == "LaserProjectile" || collision.gameObject.tag == "MissileProjectile" || 
			collision.gameObject.tag == "Pyramid" || collision.gameObject.tag == "Diamond" ||
			collision.gameObject.tag == "Star" || collision.gameObject.tag == "Cube") {
			foreach (Transform child in transform) {
				foreach (Transform subChild in child) {
					if (subChild.tag == "LaserBeam") {
						subChild.gameObject.SetActive (false);
					}
				}
			}
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "BallProjectile" || other.tag == "ShotgunProjectile" ||
			other.tag == "LaserProjectile" || other.tag == "MissileProjectile" || 
			other.tag == "Pyramid" || other.tag == "Diamond" ||
			other.tag == "Star" || other.tag == "Cube") {
			foreach (Transform child in transform) {
				foreach (Transform subChild in child) {
					if (subChild.tag == "LaserBeam") {
						subChild.gameObject.SetActive (false);
					}
				}
			}
		}
	}
}
