using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class MainMenuGUIController : MonoBehaviour
{
	private bool settingMenu = false;
	private float defaultVolume;
	private float currentSFXVol;
	private float currentMusicVol;
	public GUIStyle achievmentButtonStyle = new GUIStyle ();
	public GUIStyle leaderboardButtonStyle = new GUIStyle ();
	public GUIStyle settingButtonStyle = new GUIStyle ();
	public GUIStyle background = new GUIStyle ();
	public GUIStyle frameStyle = new GUIStyle ();
	public GUIStyle buttonStyle = new GUIStyle ();
	public GUIStyle label = new GUIStyle ();
	public GUIStyle slider = new GUIStyle ();

	void Start ()
	{
		currentMusicVol = PlayerPrefs.HasKey ("musicvol") ? PlayerPrefs.GetFloat ("musicvol") : 0.5f;

		defaultVolume = audio.volume;
	}

	void OnGUI ()
	{
		currentSFXVol = PlayerPrefs.HasKey ("sfxvol") ? PlayerPrefs.GetFloat ("sfxvol") : 0.5f;

		float size = Screen.width * 0.07f;
		achievmentButtonStyle.fixedHeight = size;
		achievmentButtonStyle.fixedWidth = size;
		
		leaderboardButtonStyle.fixedHeight = size;
		leaderboardButtonStyle.fixedWidth = size;
		
		settingButtonStyle.fixedHeight = size;
		settingButtonStyle.fixedWidth = size;
		
		float frameSize = Screen.height * 0.8f;
		
		frameStyle.fixedHeight = frameSize;
		frameStyle.fixedWidth = frameSize;
		
		buttonStyle.fixedWidth = frameSize * 0.8f;
		buttonStyle.fixedHeight = buttonStyle.fixedWidth / 23 * 6;
		buttonStyle.fontSize = (int)(buttonStyle.fixedHeight * 0.5);
		
		label.fixedHeight = size;
		label.fixedWidth = size;
		label.fontSize = (int)(buttonStyle.fixedHeight * 0.4);
		
		slider.fixedHeight = buttonStyle.fixedHeight;
		slider.fixedWidth = buttonStyle.fixedWidth * 0.5f;
		
		audio.volume = defaultVolume * currentSFXVol;

		if (settingMenu) {
			GUI.Box (new Rect (0, 0, Screen.width, Screen.height), "", background);
			
			GUI.Box (new Rect (Screen.width / 2 - frameStyle.fixedWidth / 2, Screen.height / 2 - frameStyle.fixedWidth / 2, frameStyle.fixedWidth, frameStyle.fixedHeight), "", frameStyle);
			
			GUI.Label (new Rect (Screen.width / 2 - buttonStyle.fixedWidth / 2.3f, Screen.height / 2 - buttonStyle.fixedHeight / 0.8f, label.fixedWidth, label.fixedHeight), "SFX", label);
			
			GUI.Label (new Rect (Screen.width / 2 - buttonStyle.fixedWidth / 2.3f, Screen.height / 2 - buttonStyle.fixedHeight / 2f, label.fixedWidth, label.fixedHeight), "Music", label);
			
			currentMusicVol = GUI.HorizontalSlider (new Rect (Screen.width / 2 - slider.fixedWidth / 8, Screen.height / 2 - slider.fixedHeight / 5f, slider.fixedWidth, slider.fixedHeight), currentMusicVol, 0.0f, 1.0f);
			currentSFXVol = GUI.HorizontalSlider (new Rect (Screen.width / 2 - slider.fixedWidth / 8, Screen.height / 2 - slider.fixedHeight / 1.05f, slider.fixedWidth, slider.fixedHeight), currentSFXVol, 0.0f, 1.0f);

			PlayerPrefs.SetFloat ("musicvol", currentMusicVol);
			PlayerPrefs.SetFloat ("sfxvol", currentSFXVol);

			PlayerPrefs.Save ();

			if (GUI.Button (new Rect (Screen.width / 2 - buttonStyle.fixedWidth / 2, Screen.height / 2 + buttonStyle.fixedHeight / 1.6f, buttonStyle.fixedWidth, buttonStyle.fixedHeight), "Back", buttonStyle)) {
				settingMenu = false;				
				GameObject.FindWithTag ("MainMenuController").GetComponent<MainMenuController> ().SetIsRunning ();
				audio.Play ();
			}
		} else {
			if (GUI.Button (new Rect (Screen.width - settingButtonStyle.fixedWidth, 0, settingButtonStyle.fixedWidth, settingButtonStyle.fixedHeight), "", settingButtonStyle)) {
				settingMenu = true;
				GameObject.FindWithTag ("MainMenuController").GetComponent<MainMenuController> ().SetIsRunning ();
				audio.Play ();
			}
			if (GUI.Button (new Rect (Screen.width - leaderboardButtonStyle.fixedWidth / 0.5f, 0, leaderboardButtonStyle.fixedWidth, leaderboardButtonStyle.fixedHeight), "", leaderboardButtonStyle)) {
				audio.Play ();
			}
			if (GUI.Button (new Rect (Screen.width - achievmentButtonStyle.fixedWidth / 0.33f, 0, achievmentButtonStyle.fixedWidth, achievmentButtonStyle.fixedHeight), "", achievmentButtonStyle)) {
				audio.Play ();
			}
		}
	}
}
