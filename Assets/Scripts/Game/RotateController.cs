using UnityEngine;
using System.Collections;

public class RotateController : MonoBehaviour
{
	public float rotateSpeed = 100f;
	private bool stopRotate = false;
	// Update is called once per frame
	void Update ()
	{
		if (!stopRotate) {
			transform.Rotate (0, rotateSpeed * Time.deltaTime, 0);
		}
	}

	void OnCollisionEnter (Collision collision)
	{
		if (collision.gameObject.tag == "BallProjectile" || collision.gameObject.tag == "ShotgunProjectile" ||
			collision.gameObject.tag == "LaserProjectile" || collision.gameObject.tag == "MissileProjectile") {
			stopRotate = true;
		}
	}
	
	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "BallProjectile" || other.tag == "ShotgunProjectile" ||
			other.tag == "LaserProjectile" || other.tag == "MissileProjectile") {
			stopRotate = true;
		}
	}
}
