using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour
{
	public float levelSpeed;
	public float fadeSpeed;
	private Color colorStart;
	private Color colorEnd;
	private float alpha = 0.0f;

	void Update ()
	{
		transform.Translate (Vector3.back * levelSpeed * Time.deltaTime);
		alpha = Mathf.Lerp (alpha, 1, Time.deltaTime * fadeSpeed);
	}
}
