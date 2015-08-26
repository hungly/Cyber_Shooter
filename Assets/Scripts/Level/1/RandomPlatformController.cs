using UnityEngine;
using System.Collections;

public class RandomPlatformController : MonoBehaviour
{
	public int xMaximumOffset;

	// Use this for initialization
	void Start ()
	{
		transform.Translate (
			Random.Range (-xMaximumOffset, xMaximumOffset + 1),
			0.0f,
			0.0f
		);
	}
}
