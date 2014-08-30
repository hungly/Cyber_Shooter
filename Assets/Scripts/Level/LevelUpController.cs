using UnityEngine;
using System.Collections;

public class LevelUpController : MonoBehaviour
{

	void OnTriggerEnter ()
	{
		// add script to increase level here
		// might need to access level value stored inside GameController script
		GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().increaseLevel ();
	}
}
