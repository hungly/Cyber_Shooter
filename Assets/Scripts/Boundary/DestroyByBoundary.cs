using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour
{
	void OnTriggerExit (Collider other)
	{
		// clear any object that has left the game field
		Destroy (other.gameObject);
	}
}
