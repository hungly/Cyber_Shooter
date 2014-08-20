using UnityEngine;
using System.Collections;

public class RiseFloorBlock : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		foreach (Transform child in transform) {
			if (child.tag == "LevelFloor") {
				int riseAmount = Random.Range (-5, 5);
				child.Translate (Vector3.up * riseAmount);
			}
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
