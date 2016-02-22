using UnityEngine;
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
	private int currentHole {get; set;}
	
	void Start()
	{
	}
	
	void Update()
	{
		switchPlayer();
	}
	
	void switchHole()
	{
		//End the hole if it is the last hole
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

	void switchPlayer()
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

		Debug.Log("CurrentPlayer is : " + currentPlayerNumber);
	}
	
	void showScoreCard()
	{
		//TODO: Create scorecard Method
	}
	
	
}