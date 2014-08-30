using UnityEngine;
using System.Collections;

public class SettingMenuController : MonoBehaviour
{
	public GUIStyle backgroundStyle;
	public GUIStyle buttonStyle;
	public GUIStyle frameStyle;
	private bool menu = false;

	void OnGUI ()
	{
		if (menu) {

			GUI.Box (new Rect (0, 0, Screen.width, Screen.height),"", backgroundStyle);

			GUI.Box (new Rect (Screen.width / 2 - frameStyle.fixedWidth / 2, Screen.height / 2 - frameStyle.fixedHeight / 2, frameStyle.fixedWidth, frameStyle.fixedHeight), "Settings", frameStyle);

			if (GUI.Button (new Rect (Screen.width / 2 - buttonStyle.fixedWidth / 2, Screen.height / 2 + buttonStyle.fixedHeight / 2+50, buttonStyle.fixedWidth, buttonStyle.fixedHeight), "Back", buttonStyle)) {
				ToogleMenu ();
				GameObject.FindWithTag ("MainMenuController").GetComponent<MainMenuController> ().SetIsRunning ();
			}
		}
	}

	public void ToogleMenu ()
	{
		menu = !menu;
	}
}
