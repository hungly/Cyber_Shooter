using UnityEngine;
using System.Collections;

public class BirdshotProjectile : MonoBehaviour
{
	public float shotForce;
	
	void Start ()
	{
		rigidbody.AddForce(transform.forward * shotForce, ForceMode.Impulse);
	}
}
