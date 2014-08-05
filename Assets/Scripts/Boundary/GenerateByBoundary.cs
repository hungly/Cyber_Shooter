using UnityEngine;
using System.Collections;

public class GenerateByBoundary : MonoBehaviour
{
	public GameObject levelSegment;

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Level") {
			Instantiate (levelSegment, other.transform.position + Vector3.forward * 20, other.transform.rotation);
		}
	}
}
