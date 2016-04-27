using UnityEngine;
using System.Collections;

public class SpringPowerUp : powerUp
{
	//power up variables
	private int springPower = 1000;

	// Use this for initialization
	void Start () 
	{
	}

	// Update is called once per frame
	void Update () 
	{
	}

	//pass our player into the powerUp
	public override void usePowerUp(player player)
	{
		player.ball.GetComponent<Rigidbody>().AddForce(Vector3.up * (springPower));
	}
}
