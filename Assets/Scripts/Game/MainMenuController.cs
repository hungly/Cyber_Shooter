using UnityEngine;
using System.Collections;
using GooglePlayGames;

public class MainMenuController : MonoBehaviour
{
	public Transform levelPresenter;
	private bool isRunning = true;

	void Start(){
		PlayGamesPlatform.Activate ();
		GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().setMaxLevelAtStart();
	}

	// Update is called once per frame
	void Update ()
	{
		if (isRunning) {
			if (Input.touchCount > 0) {
				Touch touchPoint = Input.GetTouch (0);

				if (touchPoint.phase == TouchPhase.Moved) {
					levelPresenter.position = new Vector3 (
					Mathf.Clamp (transform.position.x - touchPoint.deltaPosition.x * 0.1f, -5, -29),
					levelPresenter.position.y,
					levelPresenter.position.z
					);
				}

				Ray ray = Camera.main.ScreenPointToRay (touchPoint.position);
				RaycastHit hit = new RaycastHit ();
				if (Physics.Raycast (ray, out hit, 10)) {
					// touch a level label
				
					if (touchPoint.phase == TouchPhase.Ended && hit.collider.tag.StartsWith ("Level")) {
						Application.LoadLevel (hit.collider.tag);
					}
				}
				
			}
		}
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
