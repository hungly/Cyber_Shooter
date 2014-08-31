using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class GameMenuController : MonoBehaviour
{
	private bool pauseButton = true;
	private bool pauseMenu = false;
	private bool settingMenu = false;
	private bool gameOverScreen = false;
	private float defaultVolume;
	private float currentSFXVol;
	private float currentMusicVol;
	public GUIStyle pauseButtonStyle = new GUIStyle ();
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
		pauseButtonStyle.fixedHeight = size;
		pauseButtonStyle.fixedWidth = size;
		
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

		if (pauseButton) {
			if (GUI.Button (new Rect (Screen.width - pauseButtonStyle.fixedWidth, 0, pauseButtonStyle.fixedWidth, pauseButtonStyle.fixedHeight), "", pauseButtonStyle)) {
				pauseMenu = true;
				pauseButton = false;
				Time.timeScale = 0;
				GameObject.FindWithTag ("GameController").GetComponent<GameController> ().changeGamePausedStatus ();
				audio.Play ();
			}
		}

		if (pauseMenu) {
			GUI.Box (new Rect (0, 0, Screen.width, Screen.height), "", background);

			GUI.Box (new Rect (Screen.width / 2 - frameStyle.fixedWidth / 2, Screen.height / 2 - frameStyle.fixedWidth / 2, frameStyle.fixedWidth, frameStyle.fixedHeight), "", frameStyle);

			if (GUI.Button (new Rect (Screen.width - pauseButtonStyle.fixedWidth, 0, pauseButtonStyle.fixedWidth, pauseButtonStyle.fixedHeight), "", settingButtonStyle)) {
				pauseMenu = false;
				settingMenu = true;
				audio.Play ();
			}

			if (GUI.Button (new Rect (Screen.width / 2 - buttonStyle.fixedWidth / 2, Screen.height / 2 - buttonStyle.fixedHeight / 0.6f, buttonStyle.fixedWidth, buttonStyle.fixedHeight), "Resume", buttonStyle)) {
				pauseMenu = false;
				pauseButton = true;
				Time.timeScale = 1;
				GameObject.FindWithTag ("GameController").GetComponent<GameController> ().changeGamePausedStatus ();
				audio.Play ();
			}

			if (GUI.Button (new Rect (Screen.width / 2 - buttonStyle.fixedWidth / 2, Screen.height / 2 - buttonStyle.fixedHeight / 2, buttonStyle.fixedWidth, buttonStyle.fixedHeight), "Restart", buttonStyle)) {
				Time.timeScale = 1;
				Application.LoadLevel ("Level 1");
				audio.Play ();
			}

			if (GUI.Button (new Rect (Screen.width / 2 - buttonStyle.fixedWidth / 2, Screen.height / 2 + buttonStyle.fixedHeight / 1.5f, buttonStyle.fixedWidth, buttonStyle.fixedHeight), "Quit", buttonStyle)) {
				Time.timeScale = 1;
				Application.LoadLevel ("Main Menu");
				audio.Play ();
			}
		}

		if (settingMenu) {
			GUI.Box (new Rect (0, 0, Screen.width, Screen.height), "", background);

			GUI.Box (new Rect (Screen.width / 2 - frameStyle.fixedWidth / 2, Screen.height / 2 - frameStyle.fixedWidth / 2, frameStyle.fixedWidth, frameStyle.fixedHeight), "", frameStyle);

			label.alignment = TextAnchor.MiddleLeft;

			GUI.Label (new Rect (Screen.width / 2 - buttonStyle.fixedWidth / 2.3f, Screen.height / 2 - buttonStyle.fixedHeight / 0.8f, label.fixedWidth, label.fixedHeight), "SFX", label);
			
			GUI.Label (new Rect (Screen.width / 2 - buttonStyle.fixedWidth / 2.3f, Screen.height / 2 - buttonStyle.fixedHeight / 2f, label.fixedWidth, label.fixedHeight), "Music", label);
			
			currentMusicVol = GUI.HorizontalSlider (new Rect (Screen.width / 2 - slider.fixedWidth / 8, Screen.height / 2 - slider.fixedHeight / 5f, slider.fixedWidth, slider.fixedHeight), currentMusicVol, 0.0f, 1.0f);
			currentSFXVol = GUI.HorizontalSlider (new Rect (Screen.width / 2 - slider.fixedWidth / 8, Screen.height / 2 - slider.fixedHeight / 1.05f, slider.fixedWidth, slider.fixedHeight), currentSFXVol, 0.0f, 1.0f);
			
			PlayerPrefs.SetFloat ("musicvol", currentMusicVol);
			PlayerPrefs.SetFloat ("sfxvol", currentSFXVol);
			PlayerPrefs.Save ();
			
			if (GUI.Button (new Rect (Screen.width / 2 - buttonStyle.fixedWidth / 2, Screen.height / 2 + buttonStyle.fixedHeight / 1.6f, buttonStyle.fixedWidth, buttonStyle.fixedHeight), "Back", buttonStyle)) {
				settingMenu = false;				
				pauseMenu = true;
				audio.Play ();
			}
		}

		if (gameOverScreen) {
			Time.timeScale=0;
			//GameObject.FindWithTag ("GameController").GetComponent<GameController> ().changeGamePausedStatus ();

			GUI.Box (new Rect (0, 0, Screen.width, Screen.height), "", background);
			
			GUI.Box (new Rect (Screen.width / 2 - frameStyle.fixedWidth / 2, Screen.height / 2 - frameStyle.fixedWidth / 2, frameStyle.fixedWidth, frameStyle.fixedHeight), "", frameStyle);

			label.alignment = TextAnchor.MiddleCenter;

			GUI.Label (new Rect (Screen.width / 2 - label.fixedWidth / 2, Screen.height / 2 - label.fixedHeight * 3, label.fixedWidth, label.fixedHeight), "GAME OVER", label);
			
			GUI.Label (new Rect (Screen.width / 2 - label.fixedWidth / 2, Screen.height / 2 - label.fixedHeight * 1.6f, label.fixedWidth, label.fixedHeight),
			           "You survived\n\n" + GameObject.FindWithTag ("GameController").GetComponent<GameController> ().GetPlayedTime () + " s", 
			           label);

			if (GUI.Button (new Rect (Screen.width / 2 - buttonStyle.fixedWidth / 2, Screen.height / 2 - buttonStyle.fixedHeight / 9f, buttonStyle.fixedWidth, buttonStyle.fixedHeight), "Restart", buttonStyle)) {
				Time.timeScale = 1;
				TootgleGameOverScreen();
				Application.LoadLevel ("Level 1");
				GameObject.FindWithTag ("GameController").GetComponent<GameController> ().changeGamePausedStatus ();
				audio.Play ();
			}

			if (GUI.Button (new Rect (Screen.width / 2 - buttonStyle.fixedWidth / 2, Screen.height / 2 + buttonStyle.fixedHeight, buttonStyle.fixedWidth, buttonStyle.fixedHeight), "Quit", buttonStyle)) {
				Time.timeScale = 1;
				TootgleGameOverScreen();
				Application.LoadLevel ("Main Menu");
				GameObject.FindWithTag ("GameController").GetComponent<GameController> ().changeGamePausedStatus ();
				audio.Play ();
			}
		}
	}

	public void TootgleGameOverScreen ()
	{
		gameOverScreen = !gameOverScreen;
	}
}
