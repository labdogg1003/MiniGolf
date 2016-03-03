using UnityEngine;
using System.Collections;

public enum powerupType{spring};

public class powerUp : MonoBehaviour
{
	public powerupType powerUpType;
	float ShrinkScale = 0.00001f;
	float GrowScale = 0.019f;
	float shrinkSpeed = 10f;
	float growSpeed = 5f;

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
			StartCoroutine(checkActive());
		}	
	}

	IEnumerator checkActive()
	{
		float elapsedTime = 0;

		if (!active)
		{
			while(elapsedTime < shrinkSpeed)
			{
				this.transform.localScale = Vector3.Lerp(this.transform.localScale, new Vector3(ShrinkScale, ShrinkScale, ShrinkScale), elapsedTime / shrinkSpeed);
				elapsedTime += Time.deltaTime;
				yield return new WaitForEndOfFrame();
			}
		}

		if (active)
		{
			while(elapsedTime < growSpeed)
			{
				this.transform.localScale = Vector3.Lerp(this.transform.localScale, new Vector3(GrowScale,GrowScale,GrowScale),elapsedTime / growSpeed);
				elapsedTime += Time.deltaTime;
				yield return new WaitForEndOfFrame();
			}
		}
	}

	public void respawn()
	{
		active = true;
		checkActive();
	}
}

