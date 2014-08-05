using UnityEngine;
using System.Collections;

public class BirdshotProjectile : MonoBehaviour
{
	public float projectileSpeed;
	
	void Start ()
	{
		rigidbody.velocity = transform.forward * projectileSpeed;
	}
}
