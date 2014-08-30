using UnityEngine;
using System.Collections;

public class ResumeGameController : MonoBehaviour
{
	public GameObject pauseButton;
	public Texture2D normalTexture;
	public Texture2D pressedTexture;
	// Update is called once per frame
	void Update ()
	{
		for (int i = 0; i < Input.touchCount; i++) {
			if (guiTexture.HitTest (Input.GetTouch (i).position)) {
				if (Input.GetTouch (i).phase == TouchPhase.Ended || Input.GetTouch (i).phase == TouchPhase.Canceled || Input.GetTouch (i).phase == TouchPhase.Moved) {
					guiTexture.texture = normalTexture;
					if (Input.GetTouch (i).phase == TouchPhase.Ended) {
						if (Time.timeScale != 1) {
							Time.timeScale = 1;
							pauseButton.SetActive (true);
							gameObject.transform.parent.gameObject.SetActive (false);
							GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().changeGamePausedStatus ();
							break;
						}
					}
				} else {
					guiTexture.texture = pressedTexture;
				}
			}
		}
	}
}