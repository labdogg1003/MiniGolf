using UnityEngine;
using System.Collections;

public enum powerupType{spring};

public class powerUp : MonoBehaviour
{
	public powerupType powerUpType;

	//power up variables
	public int springPower = 1000;


	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	//pass our player into the powerUp
	public void usePowerUp(player player)
	{
		switch (powerUpType)
		{
			case powerupType.spring:
				player.ball.GetComponent<Rigidbody>().AddForce(Vector3.up * (springPower));
				break;
			default:
				break;
		}
	}

	void OnTriggerEnter(Collider obj)
	{
		Debug.Log(obj.tag);

		if(obj.tag == "ball")
		{
			Debug.Log("Trigger Hit");

			obj.GetComponentInParent<player>().addPowerUp(this);
		}
		
	}
}

