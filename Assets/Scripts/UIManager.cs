using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIManager : MonoBehaviour 
{
	//Get our UI Element
	public GameObject GameUI;
	private List<GameObject> Children = new List<GameObject>();


	// Use this for initialization
	void Start () 
	{
		//Grab all UI panels that we have access to.
		foreach (Transform child in GameUI.transform)
		{
			Children.Add(child.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		//dynamicUIPosition(GameUI.transform.FindChild("InGameUI").FindChild("TopLeft").gameObject, new Vector2(70,Screen.height-50));
	}
		
	void SwitchUIElement(string ui_element)
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
		
	bool checkCurrentUIElement(string ui_element)
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

	//TODO: Move to UI Manager
	void showCurrentPowerUp(player currentPlayer)
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


	void dynamicUIPosition(GameObject screenPanel, Vector2 newPosition)
	{
		//screenPanel.transform.position = new Vector3(Screen.width/2, Screen.height/2,0);
		screenPanel.transform.position = newPosition;
	}
}
