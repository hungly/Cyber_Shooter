using UnityEngine;
using System.Collections;

public class LaserProjectile : MonoBehaviour
{
	public float speed;
	private float defaultVolume;

	void Start ()
	{
		rigidbody.velocity = transform.forward * speed;
		defaultVolume = audio.volume;
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag != "Player" && other.tag != "Boundary" && other.tag != "LevelSegment" 
			&& other.tag != "Trigger" && other.tag != "PopupBlock" && other.tag != "LaserBeam") {
			Destroy (gameObject,1);
			if (other.rigidbody != null) {
				other.rigidbody.AddForce (rigidbody.velocity, ForceMode.Impulse);
			}
			audio.volume = defaultVolume * (PlayerPrefs.HasKey ("sfxvol") ? PlayerPrefs.GetFloat ("sfxvol") : 0.5f);
			audio.Play ();
		}
	}
}
