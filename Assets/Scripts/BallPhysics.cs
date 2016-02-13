using UnityEngine;
using System.Collections;

public class BallPhysics : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		GetComponent<Rigidbody>().sleepThreshold = 5f;
		GetComponent<Rigidbody>().maxAngularVelocity = 30f;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate()
	{
		//GetComponent<Rigidbody>().AddForce(-Vector3.up * 9.8f);
	}

	public void OnCollisionStay(Collision collisionInfo)
	{
		//Debug.Log(GetComponent<Rigidbody>().velocity.x + GetComponent<Rigidbody>().velocity.y + GetComponent<Rigidbody>().velocity.z);
		if (Mathf.Abs((GetComponent<Rigidbody>().velocity.x + GetComponent<Rigidbody>().velocity.y + GetComponent<Rigidbody>().velocity.z)) > 0.1f) {
			//if ((GetComponent<Rigidbody>().velocity.x + GetComponent<Rigidbody>().velocity.y + GetComponent<Rigidbody>().velocity.z) < 1.2) {
				//GetComponent<Rigidbody>().angularDrag += Time.deltaTime ;
				//GetComponent<Rigidbody>().drag += Time.deltaTime * .05f ;
			//}
		} else if(GetComponent<Rigidbody>().IsSleeping())
		{
			//GetComponent<Rigidbody>().drag = .05f;
			
		}
		if (!GetComponent<Rigidbody>().IsSleeping() && (GetComponent<Rigidbody>().velocity.x + GetComponent<Rigidbody>().velocity.y + GetComponent<Rigidbody>().velocity.z) < .1f) 
		{
			
		}
	}
}
