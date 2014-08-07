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
		FadeAll ();
	}

	void FadeAll ()
	{
		foreach (Transform o in transform) {
			FadeInObject(o);
		}
	}

	void FadeInObject (Transform o)
	{
		Color color = new Color (o.renderer.material.color.r, o.renderer.material.color.g,
		                        o.renderer.material.color.b, alpha);
		o.renderer.material.color = color;
	}
}
