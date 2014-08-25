using UnityEngine;
using System.Collections;

public class FallingColumnController : MonoBehaviour
{

	public GameObject column;

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player") {
			column.SetActive (true);

			Debug.Log (other.transform.position);

			column.transform.Translate (other.transform.position.x, 0.0f, 0.0f);
		}
	}
}
