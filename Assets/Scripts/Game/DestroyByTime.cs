using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour
{
	void Start ()
	{
		Destroy (gameObject, 5);
	}
}
