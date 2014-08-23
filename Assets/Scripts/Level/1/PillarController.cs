using UnityEngine;
using System.Collections;

public class PillarController : MonoBehaviour
{
	public float minimumSpeed;
	public float maximumSpeed;
	public int minimumXOffsetAmount;
	public int maximumXOffsetAmount;
	public int yOffsetAmount;
	public int zOffsetAmount;
	private float xPosition;
	private int xSide;
	private float zPosition;
	private float maximumHeight;
	private float yTarget;
	private float speed;
	private float initalHeight;

	// Use this for initialization
	void Start ()
	{
		initalHeight = transform.position.y;
		xSide = Random.Range (-1, 1) == 0 ? 1 : -1;
		xPosition = Random.Range (minimumXOffsetAmount, maximumXOffsetAmount + 1) * xSide;
		zPosition = Random.Range (-zOffsetAmount, zOffsetAmount + 1);
		maximumHeight = Random.Range (-yOffsetAmount, yOffsetAmount + 1) - 5;
		speed = Random.Range (minimumSpeed, maximumSpeed + 1);
		
		transform.Translate (new Vector3 (xPosition, 0.0f, zPosition));

		yTarget = maximumHeight;
	}
	
	// Update is called once per frame
	void Update ()
	{
		float step = speed * Time.deltaTime;
	
		if (transform.position.y != yTarget)
			transform.position = Vector3.MoveTowards (
				transform.position, 
				new Vector3 (transform.position.x, yTarget, transform.position.z),
				step);
		else
			yTarget = transform.position.y == maximumHeight ? initalHeight : transform.position.y == initalHeight ? maximumHeight : yTarget;
	}
}
