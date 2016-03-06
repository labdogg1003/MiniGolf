using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/*
 *	This is our game manager that handles all switching of
 *  camera between players. It also tells us what hole we
 *  are on in the array
 */

public class Game : MessageBehaviour
{
	
	public Camera mainCamera;
	public List<player> players;
	private int currentPlayerNumber = 0;
	public player currentPlayer {get; set;}
	public Hole currentHole {get; set;}
	public List<Hole> holes;
	public UIManager UI;
	private int currentHoleInGame {get; set;}
	private List<GameObject> Children = new List<GameObject>();

	
	protected override void OnStart()
	{
		//Get Our UI Manager
		UI = this.GetComponent<UIManager>();

		//Switch To Our Game UI
		UI.SwitchUIElement("InGameUI");

		switchHole();
		switchPlayer();
		checkForOOB();
	}
	
	void Update()
	{
		//This should move to remove unnecessary calls
		//showCurrentPowerUp();
	}
	
	public void switchHole()
	{
		//End the game if it is the last hole
		if(currentHoleInGame == holes.Count)
		{
			endGame();
		}
	
	  // Check that we started our counter 
	  // else increment our current hole by 1
		if(currentHoleInGame == null)
		{
			currentHoleInGame = 0;
		}
		else
		{
			currentHoleInGame ++;
		}
		setCurrentHole();
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

		currentPlayer.gameObject.SetActive(true);

		if(currentPlayer.ball.GetComponent<Rigidbody>().IsSleeping())
		{
			checkForOOB();
		}

		//reset the hit boolean values
		mainCamera.GetComponent<Shoot>().canShoot = true;
		mainCamera.GetComponent<Shoot>().hasBeenHit = false;

		//Switch Camera to current player
		currentPlayer.camera = mainCamera.gameObject;
		currentPlayer.camera.GetComponent<OrbitCamera>().target = currentPlayer.cameraFocus;

		//Set the gui components to match our current character
		//UI.updateGameInfo(currentPlayer.name,currentPlayer.strokes.ToString(), currentHole.ToString());
		Messenger.SendToListeners(new CurrentPlayerMessage(gameObject, "UpdateCurrentPlayerInfo", currentPlayer));

		//Set the power up element in the UI.
		//UI.updateCurrentPowerUp(currentPlayer); //This is got merged into the call above;
	}

	void setCurrentHole()
	{
		currentHole = holes[currentHoleInGame - 1];
		foreach(player p in players)
		{
			p.reset();
		}
	}

	public void checkForOOB()
	{
		float distance;
		RaycastHit hit;

		foreach(player p in players)
		{
			distance = .01f;
			distance += p.ball.GetComponent<SphereCollider>().radius;

			Debug.DrawRay(p.ball.transform.position, Vector3.down *distance, Color.cyan);

			if(Physics.Raycast(new Ray(p.ball.transform.position,Vector3.down), out hit, distance))
			{
				if(hit.collider.transform.GetComponentInParent<Hole>().holeName != currentHole.GetComponent<Hole>().holeName)
				{
					Debug.Log("On the wrong course :" 
						+ hit.collider.transform.GetComponentInParent<Hole>().holeName 
						+ " != "
						+ currentHole.GetComponent<Hole>().holeName);
					p.reset();
				}
			}
			else
			{
				p.reset();
			}
		}

		//Set our last starting position to where we are now.
		//This will be changed if we are out of bounds
		currentPlayer.lastStartingPosition = currentPlayer.ball.transform.position;
	}

	void showScoreCard()
	{
		//TODO: Create scorecard Method
	}
}