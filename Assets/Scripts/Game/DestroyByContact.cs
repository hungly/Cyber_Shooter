using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour
{
	private GameObject effect;
	private bool isDestroy = false;

	void Start ()
	{
		if (rigidbody != null) {
			rigidbody.useGravity = false;
		}
	}

	void OnCollisionEnter (Collision collision)
	{
		if (collision.gameObject.tag == "BallProjectile" || collision.gameObject.tag == "ShotgunProjectile" ||
			collision.gameObject.tag == "LaserProjectile" || collision.gameObject.tag == "MissileProjectile" ||
			collision.gameObject.tag == "Player" || 
			collision.gameObject.tag == "Pyramid" || collision.gameObject.tag == "Diamond" ||
			collision.gameObject.tag == "Star" || collision.gameObject.tag == "Cube") {
			Destroy (gameObject, 1);

			if (!isDestroy) {
				GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().increaseBullet (gameObject.tag);
				GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().increaseItemShotConsecutivelyWithoutBeingHit ();
				isDestroy = true;
			}

			if (collision.gameObject.tag == "Pyramid" || collision.gameObject.tag == "Star" ||
				collision.gameObject.tag == "Diamond") {
				GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().increaseAccurateShot ();
				GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().increaseItemShotConsecutivelyWithoutBeingHit ();
			} else {
				GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().increaseMissedShot ();
			}

			if (collision.gameObject.tag == "Player") {
				GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().resetItemShotConsecutivelyWithoutBeingHit ();
			}

			effect = (GameObject)Resources.Load ("Effects/Levels/Destroy " + GameObject.FindWithTag ("GameController").GetComponent<GameController> ().GetLevel ());
			Instantiate (effect, transform.position, Quaternion.identity);
		}
		
		if (rigidbody != null && collision.gameObject.tag != "LevelFloor") {
			rigidbody.useGravity = true;
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "BallProjectile" || other.tag == "ShotgunProjectile" ||
			other.tag == "LaserProjectile" || other.tag == "MissileProjectile" ||
			other.tag == "Player" || 
			other.tag == "Pyramid" || other.tag == "Diamond" ||
			other.tag == "Star" || other.tag == "Cube") {
			Destroy (gameObject, 1);
			
			if (!isDestroy) {
				GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().increaseBullet (gameObject.tag);
				GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().increaseItemShotConsecutivelyWithoutBeingHit ();
				isDestroy = true;
			}

			effect = (GameObject)Resources.Load ("Effects/Levels/Destroy " + GameObject.FindWithTag ("GameController").GetComponent<GameController> ().GetLevel ());
			Instantiate (effect, transform.position, Quaternion.identity);
		}
		
		if (rigidbody != null && other.tag != "LevelFloor") {
			rigidbody.useGravity = true;
		}
	}

	public void TriggerDestroy(Collider other){
		OnTriggerEnter (other);
	}
}
