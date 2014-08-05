using UnityEngine;
using System.Collections;

public class MissileProjectile : MonoBehaviour
{
	public float enginePower;
	public float explosionForce;
	public float explosionRadius;
	
	void Start ()
	{
		rigidbody.useGravity = true;
		rigidbody.AddForce ((transform.forward) * 10, ForceMode.Impulse);
		StartCoroutine (turnOneEngine ());
	}

	void OnCollisionEnter (Collision collision)
	{
		// if the bullet hit something that has a rigidbody
		Destroy (gameObject);
		
		Collider[] colliders = Physics.OverlapSphere (collision.contacts [0].point, explosionRadius);
		
		foreach (Collider c in colliders) {
			if (c.rigidbody != null) {
				c.rigidbody.AddExplosionForce (explosionForce, collision.contacts [0].point, 3, 0, ForceMode.Impulse);
			}
		}
	}

	IEnumerator turnOneEngine ()
	{
		yield return new WaitForSeconds (0.5f);
		rigidbody.useGravity = false;
		rigidbody.velocity = Vector3.zero;
		// add engine animation here and enjoy result
		rigidbody.AddForce(transform.forward * enginePower, ForceMode.Acceleration);
	}
}
