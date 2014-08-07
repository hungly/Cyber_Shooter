using UnityEngine;
using System.Collections;

public class LaserProjectile : MonoBehaviour
{
	public float speed;

	void Start ()
	{
		rigidbody.velocity = transform.forward * speed;
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag != "Player" && other.tag != "Boundary") {
			Destroy (gameObject);
		}
	}
}
