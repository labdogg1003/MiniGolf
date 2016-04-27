using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIManager : MessageBehaviour
{
	//Get our UI Element
	private GameObject GameUI;
	private List<GameObject> Children = new List<GameObject>();

	// Use this for initialization
	protected override void OnStart () 
	{
		//Grab our game UI by tag
		GameUI = GameObject.FindGameObjectWithTag("UI");

		//Grab all UI panels that we have access to.
		foreach (Transform child in GameUI.transform)
		{
			Children.Add(child.gameObject);
		}

		Messenger.RegisterListener(new Listener("UpdateCurrentPlayerInfo", gameObject, "HandlePlayerInfoChange"));
		Messenger.RegisterListener(new Listener("UpdateCurrentPlayerInfo", gameObject, "HandlePowerUpChange"));

		//Messenger.RegisterListener(new Listener("UpdateHoleInfo", gameObject, "HandleHoleInfoChange"));
	}
	
	// Update is called once per frame
	void Update () 
	{
	}
		
	public void SwitchUIElement(string ui_element)
	{
		int element_num = -1;

		//check that the element exists
		for (int i = 0; i < Children.Count; i++) 
		{
			if (Children[i].name == ui_element) 
			{
				//return the position in the array
				element_num = i;
				break;           
			} 
		}

		//Check that we found our element
		if (element_num != -1) 
		{
			//Iterate from 0 to count
			for (int i = 0; i < Children.Count; i++) 
			{
				if(i == element_num)
				{
					Children[i].gameObject.SetActive(true);
				}
				else
				{
					Children[i].gameObject.SetActive(false);
				}
			}
		}
	}
		
	public bool checkCurrentUIElement(string ui_element)
	{
		int element_num = -1;

		//check that the element exists
		for (int i = 0; i < Children.Count; i++) 
		{
			if (Children[i].name == ui_element) 
			{
				//return the position in the array
				element_num = i;
				break;           
			} 
		}

		if(element_num == -1)
		{
			return false;
		}
		else
		{
			return Children[element_num].gameObject.activeSelf;
		}
	}
		
	public void HandlePowerUpChange(CurrentPlayerMessage m)
	{
		Image powerUpImage = GameUI.transform.FindChild("InGameUI").FindChild("PowerMeter").FindChild("PowerUp").GetComponent<Image>();
		string powerUpName;

		//get the current player and see what their current powerup is
		if(m.CurrentPlayer.PowerUps[0] != null)
		{
			powerUpName = m.CurrentPlayer.PowerUps[0].ToString();
		}
		else
		{
			powerUpName = "null";
		}

		//load that sprite into the power up texture.
		switch(powerUpName)
		{
		case "springPowerUp (SpringPowerUp)":
				{
					powerUpImage.overrideSprite = Resources.Load<Sprite>("PowerUpTextures/SpringPowerUpTexture");
					break;	
				}
			default:
				powerUpImage.overrideSprite = null;
				break;
		}
	}

	public void updatePowerMeter(float power)
	{
		//reference our game info
		Image PowerMeter = GameUI.transform.FindChild("InGameUI").FindChild("PowerMeter").FindChild("PowerMeterFill").GetComponent<Image>();

		//set our power meter
		PowerMeter.fillAmount = power;
	}

	public void HandlePlayerInfoChange(CurrentPlayerMessage m)
	{
		//reference our game info
		Text nameText = GameUI.transform.FindChild("InGameUI").FindChild("GameInfo").FindChild("Name_tag")
			.FindChild("Name").GetComponent<Text>();
		Text strokeText = GameUI.transform.FindChild("InGameUI").FindChild("GameInfo").FindChild("stroke_tag")
			.FindChild("stroke").GetComponent<Text>();

		//set our game info
		nameText.text = m.CurrentPlayer.name;
		strokeText.text = m.CurrentPlayer.strokes.ToString();
	}

	public void HandleHoleInfoChange(HoleInfoMessage m)
	{
		Debug.Log("Player Info Changed");

		//reference our game info
		Text courseText = GameUI.transform.FindChild("InGameUI").FindChild("GameInfo").FindChild("course_tag")
			.FindChild("course").GetComponent<Text>();

		//set our hole info
		courseText.text = m.Hole;
	}
}
