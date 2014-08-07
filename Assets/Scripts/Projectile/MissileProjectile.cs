using UnityEngine;
using System.Collections;

public class MissileProjectile : MonoBehaviour
{
	public float shotForce;
	public float engineForce;
	public float maxSpeed;
	public float explosionForce;
	public float explosionRadius;

	void Start ()
	{
		rigidbody.useGravity = true;
		rigidbody.AddForce (transform.forward * shotForce, ForceMode.Impulse);
		StartCoroutine (StartEngine ());
	}

	void FixedUpdate ()
	{
		if (rigidbody.velocity.z < maxSpeed) {
			rigidbody.AddForce (transform.forward * engineForce, ForceMode.Acceleration);
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag != "Player" && other.tag != "Boundary") {
			Destroy (gameObject);

			Collider[] colliders = Physics.OverlapSphere (rigidbody.position, explosionRadius);

			foreach (Collider c in colliders) {
				if (c.tag != "LevelWalls" && c.tag != "Player" && c.tag != "MissileProjectile" &&
				    c.rigidbody != null) {
					Debug.Log(c.tag);
					c.rigidbody.AddExplosionForce (explosionForce, rigidbody.position, explosionRadius, 0, ForceMode.Impulse);

					Destroy(c.gameObject, 3);
				}
			}
		}
	}

	IEnumerator StartEngine ()
	{
		yield return new WaitForSeconds (0.25f);
		rigidbody.useGravity = false;
		rigidbody.velocity = Vector3.zero;
	}
}
