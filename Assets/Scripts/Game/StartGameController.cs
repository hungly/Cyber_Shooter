using UnityEngine;
using System.Collections;

public class StartGameController : MonoBehaviour
{
	void Update ()
	{
		for (int i = 0; i < Input.touchCount; i++) {
			if (guiTexture.HitTest (Input.GetTouch (i).position)) {
				if (Input.GetTouch (i).phase == TouchPhase.Ended || Input.GetTouch (i).phase == TouchPhase.Canceled || Input.GetTouch (i).phase == TouchPhase.Moved) {
					if (Input.GetTouch (i).phase == TouchPhase.Ended) {
						Application.LoadLevel ("Level 1");
						Time.timeScale = 1;
					}
				}
			}
		}
	}
}
