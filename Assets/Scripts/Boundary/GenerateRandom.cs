using UnityEngine;
using System.Collections;

public class GenerateRandom : MonoBehaviour
{
	public Object[] levelSegment = Resources.LoadAll("Prefabs/Leves/5");
	private bool alreadySpawn = false;
	
	void OnTriggerEnter (Collider other)
	{
		int randomInt = Random.Range (0, levelSegment.Length - 1);
		GameObject spawnObject = (GameObject) levelSegment[randomInt];
		if (other.tag == "Boundary" && !alreadySpawn && spawnObject != null) {
			Instantiate (spawnObject, transform.position + Vector3.forward * 20, other.transform.rotation);
			alreadySpawn = true;
		}
	}
}