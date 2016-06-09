using UnityEngine;
using System.Collections;
using System;

public class AnimateAfter : MonoBehaviour 
{
	public GameObject nextAnimator;
	Animation animThis;
	Animation animThat;
	AnimateAfter aaNext;
	bool playedOnce = false;

	// Use this for initialization
	void Start () 
	{
		animThat = nextAnimator.GetComponent<Animation>();
		animThis = this.GetComponent<Animation>();

	}
	
	// Update is called once per frame
	void Update () 
	{
		aaNext = nextAnimator.GetComponent<AnimateAfter>();

		if(!animThis.isPlaying && !playedOnce)
		{
			try
			{
				aaNext.enabled = true;
			}
			catch(Exception e)
			{
				//this will fail for the last in the row.
			}

			playedOnce = true;
			animThat.Play();
		}
	}


}
