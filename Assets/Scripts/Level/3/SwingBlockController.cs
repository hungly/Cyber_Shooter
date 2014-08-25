using UnityEngine;
using System.Collections;

public class SwingBlockController : MonoBehaviour
{

	public Rigidbody arm;
	public Rigidbody baseBlock;
	private bool done = false;

	void OnTriggerEnter (Collider other)
	{
		if (!done && other.tag == "Player") {
			arm.useGravity = true;
			baseBlock.useGravity = true;
			done = true;
		}
	}
}
