using UnityEngine;
using System.Collections;

public class BallProjectileController : MonoBehaviour
{
	public float shotForce;
	private int collisionFrame;
	private float defaultVolume;

	void Start ()
	{
		rigidbody.AddForce (transform.forward * shotForce, ForceMode.Impulse);
		defaultVolume = audio.volume;
	}

	void OnCollisionEnter (Collision collision)
	{
		collisionFrame = Time.frameCount;

		if (collision.gameObject.tag == "Cube" || collision.gameObject.tag == "Diamond" ||
			collision.gameObject.tag == "Pyramid" || collision.gameObject.tag == "Star" ||
			collision.gameObject.tag == "BallProjectile" || collision.gameObject.tag == "ShotgunProjectile" ||
			collision.gameObject.tag == "MissileProjectile" || collision.gameObject.tag == "LaserProjectile") {
			audio.volume = defaultVolume * (PlayerPrefs.HasKey ("sfxvol") ? PlayerPrefs.GetFloat ("sfxvol") : 0.5f);
			audio.Play ();
		}
	}

	void OnCollisionStay (Collision collision)
	{
		if (collision.transform.tag == "LevelFloor" || collision.transform.tag == "LevelWall") {
			if (Time.frameCount - collisionFrame > 20) {
				Destroy (gameObject);
			}
		}
	}
}
