using UnityEngine;
using System.Collections;

public class powerUp : MonoBehaviour
{
	float ShrinkScale = 0.00001f;
	float GrowScale = 0.019f;
	float shrinkSpeed = .25f;
	float growSpeed = .25f;

	//power up variables
	public bool active = true;

	// Use this for initialization
	void Start () 
	{
		GrowScale = this.gameObject.transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	//pass our player into the powerUp
	public virtual void usePowerUp(player player)
	{
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
			while(elapsedTime < shrinkSpeed)
			{
				this.transform.localScale = Vector3.Lerp(this.transform.localScale, new Vector3(GrowScale, GrowScale, GrowScale), elapsedTime / shrinkSpeed);
				elapsedTime += Time.deltaTime;
				yield return new WaitForEndOfFrame();

			}
		}
	}

	public void respawn()
	{
		active = true;
		StartCoroutine(checkActive());
	}
}

