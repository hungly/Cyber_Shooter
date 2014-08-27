using UnityEngine;
using System.Collections;

public class GenerateRandom : MonoBehaviour
{
	private Object[] levelSegment;
	private bool alreadySpawn = false;

	void Start ()
	{
		levelSegment = Resources.LoadAll ("5");
	}
	
	void OnTriggerEnter (Collider other)
	{
		int randomInt = Random.Range (0, levelSegment.Length);
		GameObject spawnObject = (GameObject)levelSegment [randomInt];
		if (other.tag == "Boundary" && !alreadySpawn && spawnObject != null) {
			Instantiate (spawnObject, transform.position + Vector3.forward * 20, other.transform.rotation);
			alreadySpawn = true;
		}
	}
}