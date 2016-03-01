using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIManager : MonoBehaviour 
{
	//Get our UI Element
	private GameObject GameUI;
	private List<GameObject> Children = new List<GameObject>();

	// Use this for initialization
	void Start () 
	{
		//Grab our game UI by tag
		GameUI = GameObject.FindGameObjectWithTag("UI");

		//Grab all UI panels that we have access to.
		foreach (Transform child in GameUI.transform)
		{
			Children.Add(child.gameObject);
		}
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
		
	public void updateCurrentPowerUp(player currentPlayer)
	{
		Image powerUpImage = GameUI.transform.FindChild("InGameUI").FindChild("PowerMeter").FindChild("PowerUp").GetComponent<Image>();
		string powerUpName;

		//get the current player and see what their current powerup is
		if(currentPlayer.PowerUps[0] != null)
		{
			powerUpName = currentPlayer.PowerUps[0].powerUpType.ToString();
		}
		else
		{
			powerUpName = "null";
		}

		//load that sprite into the power up texture.
		switch(powerUpName)
		{
			case "spring":
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

	public void updateGameInfo(string name, string stroke, string hole)
	{
		//reference our game info
		Text nameText = GameUI.transform.FindChild("InGameUI").FindChild("GameInfo").FindChild("Name_tag")
			.FindChild("Name").GetComponent<Text>();
		Text strokeText = GameUI.transform.FindChild("InGameUI").FindChild("GameInfo").FindChild("stroke_tag")
			.FindChild("stroke").GetComponent<Text>();
		Text courseText = GameUI.transform.FindChild("InGameUI").FindChild("GameInfo").FindChild("course_tag")
			.FindChild("course").GetComponent<Text>();

		//set our game info
		nameText.text = name;
		strokeText.text = stroke;
		courseText.text = hole;
	}
}
