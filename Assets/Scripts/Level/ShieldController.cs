using UnityEngine;
using System.Collections;

public class ShieldController : MonoBehaviour
{
	public float speed = 1f;
	public int xOffsetLimit = 12;
	private int xTarget;
	private Vector3 currentVelocity = Vector3.zero;
	private float startTime;
	private float journeyLength;
	private Vector3 endPoint;

	// Use this for initialization
	void Start ()
	{
		xTarget = (Random.Range (-1, 1) == 0 ? 1 : -1) * xOffsetLimit;
		transform.Translate (new Vector3 (
			xTarget * -1,
			0.0f,
			0.0f
		));
		startTime = Time.time;
		endPoint = new Vector3 (xTarget, transform.localPosition.y, transform.localPosition.z);
		journeyLength = Vector3.Distance (transform.localPosition, endPoint);
	}
	
	// Update is called once per frame
	void Update ()
	{
		float distCovered = (Time.time - startTime) * speed;
		float fracJourney = distCovered / journeyLength;

		transform.localPosition = Vector3.Lerp (
			transform.localPosition,
			endPoint,
			fracJourney
		);

		if (Mathf.Abs (transform.localPosition.x - xTarget) < 0.2f) {
			xTarget *= -1;
			
			startTime = Time.time;
			endPoint = new Vector3 (xTarget, transform.localPosition.y, transform.localPosition.z);
		}
	}
}
