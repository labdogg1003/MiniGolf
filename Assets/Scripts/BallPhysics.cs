using UnityEngine;
using System.Collections;

public class BallPhysics : MonoBehaviour 
{
	public AudioClip hitSound;

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
		if(collisionInfo.gameObject.CompareTag("brick"))
		{
			AudioManager.PlaySoundEffect(hitSound,.3f,0);
		}
	}
}
