using UnityEngine;
using System.Collections;

public class MissileProjectile : MonoBehaviour
{
	public float shotForce;
	public float engineForce;
	public float maxSpeed;
	public float explosionForce;
	public float explosionRadius;
	private GameObject effect;
	private GameObject explosion;
	private float defaultVolume;

	void Start ()
	{
		rigidbody.useGravity = true;
		rigidbody.AddForce (transform.forward * shotForce, ForceMode.Impulse);
		StartCoroutine (StartEngine ());

		effect = (GameObject)Resources.Load ("Effects/Levels/Destroy White");
		explosion = (GameObject)Resources.Load ("Effects/Explosion");
		defaultVolume = audio.volume;
	}

	void FixedUpdate ()
	{
		audio.volume = defaultVolume * (PlayerPrefs.HasKey ("sfxvol") ? PlayerPrefs.GetFloat ("sfxvol") : 0.5f);

		if (rigidbody.velocity.z < maxSpeed) {
			rigidbody.AddForce (transform.forward * engineForce, ForceMode.Acceleration);
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag != "Player" && other.tag != "Boundary" && other.tag != "LevelSegment" 
			&& other.tag != "Trigger" && other.tag != "PopupBlock" && other.tag != "LaserBeam"
			&& other.tag != "MissileProjectile") {
			Destroy (gameObject);

			Instantiate (explosion, transform.position, Quaternion.identity);

			Collider[] colliders = Physics.OverlapSphere (rigidbody.position, explosionRadius);

			foreach (Collider c in colliders) {
				if (c.tag != "LevelWalls" && c.tag != "LevelWall" && c.tag != "LevelSegment" 
					&& c.tag != "LevelFloor" && c.tag != "Trigger" && c.tag != "PopupBlock"
					&& c.tag != "LaserBeam" && c.tag != "Player" && c.rigidbody != null) {
					c.rigidbody.AddExplosionForce (explosionForce, rigidbody.position, explosionRadius, 0, ForceMode.Impulse);

					//int DestroyObject = 0;

					if (c.tag != "Pyramid" && c.tag != "Diamond" && c.tag != "Star") {
						Destroy (c.gameObject, 3);
						
						Instantiate (effect, c.transform.position, Quaternion.identity);
					} else {
						//DestroyObject++;
						c.GetComponent<DestroyByContact> ().TriggerDestroy (gameObject.collider);
					}

					/*
					if (DestroyObject >= 3) {
						// award layer with "Bombadier" achievement
					}
					*/
				}
			}
		}
	}

	IEnumerator StartEngine ()
	{
		yield return new WaitForSeconds (0.25f);
		rigidbody.useGravity = false;
		rigidbody.velocity = Vector3.zero;
		audio.Play ();
	}
}
