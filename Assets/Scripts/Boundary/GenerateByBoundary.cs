using UnityEngine;
using System.Collections;

public class GenerateByBoundary : MonoBehaviour
{
	public GameObject levelSegmentA;
	public GameObject levelSegmentB;

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "LevelA") {
			Instantiate (levelSegmentB, other.transform.position + Vector3.forward * 20, other.transform.rotation);
		}
		if (other.tag == "LevelB") {
			Instantiate (levelSegmentA, other.transform.position + Vector3.forward * 20, other.transform.rotation);
		}
	}
}
