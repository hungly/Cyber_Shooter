using UnityEngine;
using System.Collections;

public class ResumeGameController : MonoBehaviour
{
	public GameObject pauseButton;
	// Update is called once per frame
	void Update ()
	{
		for (int i = 0; i < Input.touchCount; i++) {
			if (guiTexture.HitTest (Input.GetTouch (i).position) && Input.GetTouch (i).phase == TouchPhase.Ended) {
				GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().changeGamePausedStatus();
				if (Time.timeScale != 1) {
					Time.timeScale = 1;
					pauseButton.SetActive (true);
					gameObject.transform.parent.gameObject.SetActive (false);
					break;
				}
			}
		}
	}
}
