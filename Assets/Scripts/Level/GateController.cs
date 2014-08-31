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
	private float defaultVolume;

	void Start ()
	{
		defaultVolume = audio.volume;
	}

	void OnTriggerEnter (Collider other)
	{
		audio.volume = defaultVolume * (PlayerPrefs.HasKey ("sfxvol") ? PlayerPrefs.GetFloat ("sfxvol") : 0.5f);
		audio.Play ();
		if (other.tag == "Player") {
			openGate = true;
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
