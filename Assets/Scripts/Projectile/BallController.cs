using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour
{
	public float projectileSpeed;
	
	void Start ()
	{
		rigidbody.velocity = transform.forward * projectileSpeed;
	}
}
