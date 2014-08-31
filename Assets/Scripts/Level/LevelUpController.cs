using UnityEngine;
using System.Collections;

public class LevelUpController : MonoBehaviour
{
	private bool alreadyIncrease = false;

	void OnTriggerEnter (Collider other)
	{
		if (!alreadyIncrease && other.tag == "Player") {
			// add script to increase level here
			// might need to access level value stored inside GameController script
			GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().increaseLevel ();
		}
	}
}
