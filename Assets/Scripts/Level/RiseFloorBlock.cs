using UnityEngine;
using System.Collections;

public class RiseFloorBlock : MonoBehaviour
{
	void Start ()
	{
		foreach (Transform child in transform) {
			if (child.tag == "LevelFloor") {
				int riseAmount = Random.Range (-5, 5);
				child.Translate (Vector3.up * riseAmount);
			}
		}
	}
}
