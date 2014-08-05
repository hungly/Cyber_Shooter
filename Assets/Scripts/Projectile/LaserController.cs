using UnityEngine;
using System.Collections;

public class LaserController : MonoBehaviour
{
	public float projectileSpeed;
	
	void Start ()
	{
		rigidbody.velocity = transform.forward * projectileSpeed;
	}
}
