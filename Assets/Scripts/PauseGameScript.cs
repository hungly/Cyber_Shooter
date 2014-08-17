using UnityEngine;
using System.Collections;

public class PauseGameScript : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < Input.touchCount; i++) {
			if (guiText.HitTest(Input.GetTouch (i).position) && Input.GetTouch (i).phase == TouchPhase.Ended) {
				if (Time.timeScale != 0){
					Time.timeScale = 0;
					break;
				} else {
					Time.timeScale = 1;
					break;
				}
			}
		}
	}
}
