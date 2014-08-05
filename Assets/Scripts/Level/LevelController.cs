using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour 
{
	public float levelSpeed;

	void Update ()
	{
		transform.Translate (Vector3.down * levelSpeed * Time.deltaTime);
	}
}
