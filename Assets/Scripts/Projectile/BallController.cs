using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour
{
	public float shotForce;
	
	void Start ()
	{
		rigidbody.AddForce(transform.forward * shotForce, ForceMode.Impulse);
	}
}
