using UnityEngine;
using System.Collections;

public class ShieldController : MonoBehaviour
{
	public float smoothTime = 2f;
	private int xTarget;
	private Vector3 currentVelocity = Vector3.zero;

	// Use this for initialization
	void Start ()
	{
		xTarget = (Random.Range (-1, 1) == 0 ? 1 : -1) * 12;
		transform.Translate (new Vector3 (
			xTarget * -1,
			0.0f,
			0.0f
		));
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.position = Vector3.SmoothDamp (
			transform.position,
			new Vector3 (xTarget, transform.position.y, transform.position.z),
			ref currentVelocity,
			smoothTime
		);

		if (Mathf.Abs (transform.position.x - xTarget) < 0.5f) {
			xTarget *= -1;
		}
	}
}
