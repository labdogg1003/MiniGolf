using UnityEngine;
using System.Collections;

public enum powerupType{spring};

public class powerUp : MonoBehaviour
{
	public powerupType powerUpType;

	//power up variables
	private int springPower = 1000;
	public bool active = true;


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
		if(obj.tag == "ball" && active)
		{
			obj.GetComponentInParent<player>().addPowerUp(this);

			//set power up inactive
			active = false;
			checkActive();
		}	
	}

	void checkActive()
	{
		if (!active)
		{
			float targetScale = 0.0001f;
			float shrinkSpeed = 10f;
			this.transform.localScale = Vector3.Lerp(this.transform.localScale, new Vector3(targetScale, targetScale, targetScale), Time.deltaTime*shrinkSpeed);
		}

		if (active)
		{
			float targetScale = 0.019f;
			float shrinkSpeed = 5f;
			this.transform.localScale = Vector3.Lerp(this.transform.localScale, new Vector3(targetScale, targetScale, targetScale), Time.deltaTime*shrinkSpeed);
		}
	}


	public void respawn()
	{
		active = true;
		checkActive();
	}
}

