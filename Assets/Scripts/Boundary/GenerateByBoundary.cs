using UnityEngine;
using System.Collections;

public class GenerateByBoundary : MonoBehaviour
{
	public GameObject levelSegment;
	private bool alreadySpawn = false;

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Boundary" && !alreadySpawn && levelSegment != null) {
			Instantiate (levelSegment, transform.position + Vector3.forward * 20, other.transform.rotation);
			alreadySpawn = true;
		}
	}
}
