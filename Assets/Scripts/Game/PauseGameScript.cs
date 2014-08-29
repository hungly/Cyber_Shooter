using UnityEngine;
using System.Collections;

public class PauseGameScript : MonoBehaviour
{	
	public GameObject pauseMenu;
	// Update is called once per frame
	void Update ()
	{
		for (int i = 0; i < Input.touchCount; i++) {
			if (guiTexture.HitTest (Input.GetTouch (i).position) && Input.GetTouch (i).phase == TouchPhase.Ended) {
				if (Time.timeScale != 0) {
					Time.timeScale = 0;
					pauseMenu.SetActive (true);
					gameObject.SetActive (false);
					GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().changeGamePausedStatus();
					break;
				}
			}
		}
	}
}
