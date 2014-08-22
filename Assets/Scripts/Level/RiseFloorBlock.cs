using UnityEngine;
using System.Collections;

public class RiseFloorBlock : MonoBehaviour
{
	public int riseUp = 5;
	public int riseDown = 5;

	void Start ()
	{
		foreach (Transform child in transform) {
			if (child.tag == "LevelFloor") {
				int riseAmount = Random.Range (-riseUp, riseDown);
				child.Translate (Vector3.up * riseAmount);
			}
		}
	}
}
