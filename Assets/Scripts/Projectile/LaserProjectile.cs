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
		if (other.tag != "Player" && other.tag != "Boundary" && other.tag != "LevelSegment" 
			&& other.tag != "Trigger" && other.tag != "PopupBlock" && other.tag != "LaserBeam") {
			Destroy (gameObject);
			if (other.rigidbody != null) {
				other.rigidbody.AddForce (rigidbody.velocity, ForceMode.Impulse);
			}
		}
	}
}
