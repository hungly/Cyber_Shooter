using UnityEngine;
using System.Collections;

public class MoveVerticallyController : MonoBehaviour
{
	public float speed = 1f;
	private int yTarget;
	private float startTime;
	private float journeyLength;
	private Vector3 endPoint;
	
	// Use this for initialization
	void Start ()
	{
		yTarget = (Random.Range (-1, 1) == 0 ? 1 : -1) * 10;
		transform.Translate (new Vector3 (
			0.0f,
			yTarget * -1,
			0.0f
		));
		startTime = Time.time;
		endPoint = new Vector3 (transform.localPosition.x, yTarget, transform.localPosition.z);
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
		
		if (Mathf.Abs (transform.localPosition.y - yTarget) < 0.2f) {
			yTarget *= -1;
			
			startTime = Time.time;
			endPoint = new Vector3 (transform.localPosition.x, yTarget, transform.localPosition.z);
		}
	}
}
