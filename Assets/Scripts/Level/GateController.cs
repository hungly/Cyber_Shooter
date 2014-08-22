using UnityEngine;
using System.Collections;

public class GateController : MonoBehaviour
{
	public GameObject lowerGate;
	public GameObject upperGate;
	public float gateOpenAmount;
	public float gateOpenSpeed;
	private bool openGate = false;
	private float lowerGateOpenPosition;
	private float upperGateOpenPosition;

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player") {
			openGate = true;
			Debug.Log ("Open gate");
			lowerGateOpenPosition = lowerGate.transform.position.y - gateOpenAmount;
			upperGateOpenPosition = upperGate.transform.position.y + gateOpenAmount;
		}
	}

	void Update ()
	{
		if (openGate) {
			float step = gateOpenSpeed * Time.deltaTime;

			lowerGate.transform.position = Vector3.MoveTowards (
				lowerGate.transform.position, 
				new Vector3 (lowerGate.transform.position.x, lowerGateOpenPosition, lowerGate.transform.position.z),
				step
			);
			upperGate.transform.position = Vector3.MoveTowards (
				upperGate.transform.position, 
				new Vector3 (lowerGate.transform.position.x, upperGateOpenPosition, lowerGate.transform.position.z),
				step
			);
		}
	}
}
