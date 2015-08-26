using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour
{
	void OnTriggerExit (Collider other)
	{
		// clear any object that has left the game field
		Destroy (other.gameObject);
		if (other.tag == "Player" 
		    && !GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().IsGamePaused()) {
			GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().changeGamePausedStatus();
			GameObject.FindGameObjectWithTag("PauseButton").GetComponent<GameMenuController>().TootgleGameOverScreen();
			Social.ReportProgress("CgkI68ebh5kcEAIQDA",100.0f,(bool success) => {});
			Social.ReportScore((long)GameObject.FindWithTag ("GameController").GetComponent<GameController> ().GetPlayedTime (), "CgkI68ebh5kcEAIQAA", (bool successs) => {});
		}
	}
}