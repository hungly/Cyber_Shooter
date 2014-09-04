using UnityEngine;
using System.Collections;
using GooglePlayGames;

public class MainMenuController : MonoBehaviour
{
	public Transform levelPresenter;
	public AudioSource guiClick;
	private bool isRunning = true;
	private float defaultVolume;
	private Vector3 initialPoint = Vector3.zero;
	private int unlockedLevels;

	void Start ()
	{
		PlayGamesPlatform.Activate ();
		defaultVolume = audio.volume;
		unlockedLevels = PlayerPrefs.HasKey ("MaxLevel") ? PlayerPrefs.GetInt ("MaxLevel") : 1;

		for (int i = 1; i <= unlockedLevels; i++) {
			GameObject.FindWithTag ("Level " + i).SetActive (true);
		}
		for (int i = unlockedLevels+1; i <= 5; i++) {
			GameObject.FindWithTag ("Level " + i).SetActive (false);
		}
	}

	// Update is called once per frame
	void Update ()
	{
		audio.volume = defaultVolume * (PlayerPrefs.HasKey ("musicvol") ? PlayerPrefs.GetFloat ("musicvol") : 0.5f);

		if (isRunning) {
			if (Input.touchCount > 0) {
				Touch touchPoint = Input.GetTouch (0);

				Plane horPlane = new Plane (Vector3.up, Vector3.zero);
				Ray moveRay = Camera.main.ScreenPointToRay (touchPoint.position);

				if (touchPoint.phase == TouchPhase.Began) {
					initialPoint = Camera.main.ScreenToWorldPoint (new Vector3 (touchPoint.position.x, touchPoint.position.y, 10));
				}

				/*
				if (touchPoint.phase == TouchPhase.Moved) {
					Vector3 curentPoint = Camera.main.ScreenToWorldPoint (new Vector3 (touchPoint.position.x, touchPoint.position.y, 10));

					float moveAmount = (curentPoint.x - initialPoint.x) * Time.deltaTime * 5;
					Debug.Log ("Begin: " + initialPoint + " End: " + curentPoint + " Amount: " + moveAmount);
					levelPresenter.position = new Vector3 (
						Mathf.Clamp (levelPresenter.position.x + moveAmount, -29, -5),
						levelPresenter.position.y,
						levelPresenter.position.z
					);
				}
				*/

				if (touchPoint.phase == TouchPhase.Moved) {
					Debug.Log (touchPoint.deltaPosition.x);
					levelPresenter.position = new Vector3 (
						Mathf.Clamp (levelPresenter.position.x + touchPoint.deltaPosition.x * 0.01f, -29, -5),
						levelPresenter.position.y,
						levelPresenter.position.z
					);
				}

				Ray ray = Camera.main.ScreenPointToRay (touchPoint.position);
				RaycastHit hit = new RaycastHit ();
				if (Physics.Raycast (ray, out hit, 10)) {
					// touch a level label
				
					if (touchPoint.phase == TouchPhase.Ended && hit.collider.tag.StartsWith ("Level")) {
						guiClick.Play ();
						StartCoroutine (StartLevel (hit.collider.tag));
					}
				}
				
			}
		}
	}

	IEnumerator StartLevel (string levelName)
	{
		yield return new WaitForSeconds (0.5f);
		Application.LoadLevel (levelName);
	}

	public void SetIsRunning ()
	{
		isRunning = !isRunning;

		if (Time.timeScale == 1) {
			Time.timeScale = 0;
		} else if (Time.timeScale == 0) {
			Time.timeScale = 1;
		}
	}
}
