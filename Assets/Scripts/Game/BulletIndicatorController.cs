using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class BulletIndicatorController : MonoBehaviour
{
	public GUIStyle bulletIndicatorBase = new GUIStyle ();
	public GUIStyle bulletIndicatorProjectile = new GUIStyle ();
	public GUIStyle bulletIndicatorHit = new GUIStyle ();
	public GUIStyle bulletIndicatorCount = new GUIStyle ();

	void OnGUI ()
	{
		float size = Screen.width * 0.07f;
		
		bulletIndicatorBase.fixedHeight = size;
		bulletIndicatorBase.fixedWidth = size;
		
		bulletIndicatorProjectile.fixedHeight = size;
		bulletIndicatorProjectile.fixedWidth = size;
		
		bulletIndicatorHit.fixedHeight = size;
		bulletIndicatorHit.fixedWidth = size;
		
		bulletIndicatorCount.fixedHeight = size;
		bulletIndicatorCount.fixedWidth = size;
		bulletIndicatorCount.fontSize = (int)(bulletIndicatorBase.fixedHeight * 0.5);

		GUI.Label (new Rect (
			Screen.width / 2 - bulletIndicatorBase.fixedWidth / 2,
			0,
			bulletIndicatorBase.fixedWidth,
			bulletIndicatorBase.fixedHeight
		), "", bulletIndicatorBase);

		bulletIndicatorProjectile.normal.background = (Texture2D)Resources.Load ("button-projectile-" + GameObject.FindWithTag ("GameController").GetComponent<GameController> ().GetItemAchieved ());
		GUI.Label (new Rect (
			Screen.width / 2 - bulletIndicatorBase.fixedWidth / 2,
			0,
			bulletIndicatorProjectile.fixedWidth,
			bulletIndicatorProjectile.fixedHeight
		), "", bulletIndicatorProjectile);

		bulletIndicatorHit.normal.background = (Texture2D)Resources.Load ("button-projectile-process-" + GameObject.FindWithTag ("GameController").GetComponent<GameController> ().GetItemShotConsecutivelyWithoutBeingHit());
		GUI.Label (new Rect (
			Screen.width / 2 - bulletIndicatorBase.fixedWidth / 2,
			0,
			bulletIndicatorHit.fixedWidth,
			bulletIndicatorHit.fixedHeight
		), "", bulletIndicatorHit);

		GUI.Label (new Rect (
			Screen.width / 2 + bulletIndicatorBase.fixedWidth / 2,
			0,
			bulletIndicatorCount.fixedWidth,
			bulletIndicatorCount.fixedHeight
		), "" + GameObject.FindWithTag ("GameController").GetComponent<GameController> ().getBullets ()
		           , bulletIndicatorCount);
	}
}
