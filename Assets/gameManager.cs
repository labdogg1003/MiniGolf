using UnityEngine;
using System.Collections;

/*
 *	This is our game manager that handles all switching of
 *  camera between players. It also tells us what hole we
 *  are on in the array
 */

public class GameManager : MonoBehaviour
{
	
	public Camera mainCamera;
	public ArrayList players;
	private player currentPlayer {get; set;}
	public ArrayList holes;
	private int currentHole {get; set;}
	
	void Start()
	{
	}
	
	void Update()
	{
	}
	
	void switchHole()
	{
		//End the hole if it is the last hole
		if(currentHole == holes.size())
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
	
	void EndGame()
	{
		//TODO: Create the end game method
	}
	
	void addPlayer( Player player )
	{
		players.add(player);
	}
	
	void addHole( Hole hole )
	{
		holes.add(hole);
	}
	
	void showScoreCard()
	{
		//TODO: Create scorecard Method
	}
	
	
}