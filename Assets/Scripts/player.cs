using UnityEngine;
using System.Collections;

public class player : MessageBehaviour
{
	//This Might Change to the game manager handling the camera
	public GameObject camera;
	public string name = "";	

	public Transform cameraFocus;
	public Transform ball;
		
	//Holds the score for the player -- ArrayList to allow flexibility on hole count
	public ArrayList Score;
		
	//Check that user has finished the hole
	private bool hole_finished = false;

		
	//Holds the strokes for the hole, reset at the start of each hole
	public int strokes = 0; 
		
	//Allows the player to hold 2 power-ups at a time.
	public powerUp[] PowerUps = new powerUp[2];

	//Allows us to reset the ball if it goes out of bounds
	public Vector3 lastStartingPosition;
		
    // Use this for initialization
	protected override void OnStart()
	{
	}
    
    // Update is called once per frame
    void Update () 
    {
    }
    
    //TODO: Add power up manager scripts
    
    //Add Powerup to array.
    public void addPowerUp(powerUp p)
    {
      if(PowerUps[0] == null)
      {
		PowerUps[0] = p;
      }
      else
      {
		PowerUps[1] = PowerUps[0];
		PowerUps[0] = p;
      }

		if(this.gameObject.Equals(GameObject.Find("GameManager").GetComponent<Game>().currentPlayer));
		{
			//only change the ui if this player is the same as the current player
			Messenger.SendToListeners(new CurrentPlayerMessage(gameObject, "UpdateCurrentPlayerInfo", this));
		}
    }
    
    public void usePowerUp()
    {
	  if(PowerUps[0] == null)
      {
		// do nothing if no power ups.
      }
      else
      {
		//use the first power up
		PowerUps[0].usePowerUp(this);
		
		//move the last power up to the first power up slot and delete the second powerup
		PowerUps[0] = null;
		PowerUps[0] = PowerUps[1];
		PowerUps[1] = null;
      }
    }
    
	//Reset back to the last known good starting position.
	public void reset()
	{
		//Check if our last position is zero.
		if(lastStartingPosition == Vector3.zero)
		{
			lastStartingPosition = GameObject.Find("GameManager").GetComponent<Game>().
				currentHole.startPoint.gameObject.transform.position;
		}

		//Set this balls position to the last position it was "safe" at.
		this.ball.transform.position = lastStartingPosition;

		//If the last starting position is the same as the start of the whole make the 
		//current gameobject inactive so that it cant interfere with the current player
		if(lastStartingPosition == GameObject.Find("GameManager").GetComponent<Game>().
			currentHole.startPoint.gameObject.transform.position)
		{
			
			this.gameObject.SetActive(false);
		}
	}

    
	// This method adds the stroke amount to the score list
	// sets strokes back to zero and increments the hole number
    void holeReset()
    {
		lastStartingPosition = GameObject.Find("GameManager").GetComponent<Game>().currentHole.startPoint.gameObject.transform.position;

    	//add our final stroke count to the scorecard
		Score.Add(strokes);
    	
    	//Reset the finished boolean
    	hole_finished = false;
    		
    	//Reset the stroke Counter
    	strokes = 0;
    }
}