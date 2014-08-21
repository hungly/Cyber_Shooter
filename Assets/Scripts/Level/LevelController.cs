using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour
{
	public float levelSpeed = 10f;

	void Update ()
	{
		transform.Translate (Vector3.back * levelSpeed * Time.deltaTime);
	}
}
