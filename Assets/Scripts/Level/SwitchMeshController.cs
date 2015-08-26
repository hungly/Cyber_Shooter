using UnityEngine;
using System.Collections;

public class SwitchMeshController : MonoBehaviour
{

	public GameObject normal;
	public GameObject destructable;

	void OnTriggerEnter (Collider other)
	{
		if (other.tag != "Boundary" && other.tag != "PopupBlock" && other.tag != "Cube" && 
			other.tag != "LevelSegment") {
			normal.SetActive (false);
			destructable.SetActive (true);
		}
	}
}
