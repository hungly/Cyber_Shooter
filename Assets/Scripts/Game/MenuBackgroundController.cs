using UnityEngine;
using System.Collections;

public class MenuBackgroundController : MonoBehaviour {

	public GUIStyle backgroundStyle;
	
	void OnGUI ()
	{
			GUI.Box (new Rect (0, 0, Screen.width, Screen.height),"", backgroundStyle);
	}
}