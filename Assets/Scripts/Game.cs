using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/*
 *	This is our game manager that handles all switching of
 *  camera between players. It also tells us what hole we
 *  are on in the array
 */

public class Game : MonoBehaviour
{
	
	public Camera mainCamera;
	public List<player> players;
	private int currentPlayerNumber = 0;
	public player currentPlayer {get; set;}
	public ArrayList holes;
	public UIManager UI;
	private int currentHole {get; set;}
	private List<GameObject> Children = new List<GameObject>();

	
	void Start()
	{
		//Get Our UI Manager
		UI = this.GetComponent<UIManager>();

		//Switch To Our Game UI
		UI.SwitchUIElement("InGameUI");

		switchPlayer();

		UI.updateCurrentPowerUp(currentPlayer);
	}
	
	void Update()
	{
		//This should move to remove unnecessary calls
		//showCurrentPowerUp();
	}
	
	public void switchHole()
	{
		//End the ho	le if it is the last hole
		if(currentHole == holes.Count)
		{
			endGame();
		}
	
	  // Check that we started our counter 
	  // else increment our current hole by 1
		if(currentHole == null)
		{
			currentHole = 1;
		}
		else
		{
			currentHole ++;
		}
	}
	
	void endGame()
	{
		//TODO: Create the end game method
	}
	
	void addPlayer( player player )
	{
		players.Add(player);
	}
	
	void addHole( Hole hole )
	{
		holes.Add(hole);
	}

	//Shoot Script references this when it sees that 
	//the current player is done with their turn.
	public void switchPlayer()
	{
		if (currentPlayer == null) 
		{
			//check if we dont have a current player //i.e. start of game
			currentPlayerNumber = 0;

			//set our current player to the next player
			currentPlayer = (player)players[currentPlayerNumber];
		} 
		else if(currentPlayerNumber == players.Count - 1)
		{
			//set the current player back to the start
			currentPlayerNumber = 0;

			//set our current player to the next player
			currentPlayer = (player)players[currentPlayerNumber];
		}
		else
		{
			//increment the current player by 1
			currentPlayerNumber ++;

			//set our current player to the next player
			currentPlayer = (player)players[currentPlayerNumber];
		}

		//reset the hit boolean values
		mainCamera.GetComponent<Shoot>().canShoot = true;
		mainCamera.GetComponent<Shoot>().hasBeenHit = false;

		//Switch Camera to current player
		currentPlayer.camera = mainCamera.gameObject;
		currentPlayer.camera.GetComponent<OrbitCamera>().target = currentPlayer.cameraFocus;

		//Set the gui components to match our current character
		UI.updateGameInfo(currentPlayer.name,currentPlayer.strokes.ToString(), currentHole.ToString());
	
		//Set the power up element in the UI.
		UI.updateCurrentPowerUp(currentPlayer);
	}
	
	void showScoreCard()
	{
		//TODO: Create scorecard Method
	}
}