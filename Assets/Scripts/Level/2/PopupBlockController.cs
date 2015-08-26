using UnityEngine;
using System.Collections;

public class PopupBlockController : MonoBehaviour
{
	public float force = 300f;
	private bool done = false;

	void OnTriggerEnter (Collider other)
	{
		if (!done && other.tag == "Player") {
			foreach (Transform child in transform) {
				if (child.tag == "PopupBlock" && child.rigidbody != null) {
					child.rigidbody.useGravity = true;
					child.rigidbody.isKinematic = false;
					child.rigidbody.AddForce (Vector3.up * force, ForceMode.Impulse);
					done = true;
				}
			}
			
		}
	}
}
