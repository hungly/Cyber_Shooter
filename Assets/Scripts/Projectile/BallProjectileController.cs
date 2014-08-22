using UnityEngine;
using System.Collections;

public class BallProjectileController : MonoBehaviour
{
	public float shotForce;
	private int collisionFrame;

	void Start ()
	{
		rigidbody.AddForce (transform.forward * shotForce, ForceMode.Impulse);
	}

	void OnCollisionEnter (Collision collision){
			collisionFrame = Time.frameCount;
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
