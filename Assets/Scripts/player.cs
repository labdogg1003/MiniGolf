using UnityEngine;
using System.Collections;

public class player : MonoBehaviour 
{
	//This Might Change to the game manager handling the camera
	public GameObject camera;
		
	public Transform cameraFocus;
	public Transform ball;
		
	//Holds the score for the player -- ArrayList to allow flexibility on hole count
	public ArrayList Score;
		
	private bool hole_finished;
			
	//Holds the strokes for the hole, reset at the start of each hole
	private int strokes = 0; 
		
	//Allows the player to hold 2 power-ups at a time.
	public powerUp[] PowerUps = new powerUp[2];
		
    // Use this for initialization
    void Start () 
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
    
    
	// This method adds the stroke amount to the score list
	// sets strokes back to zero and increments the hole number
    void holeReset()
    {
    	//add our final stroke count to the scorecard
		Score.Add(strokes);
    	
    	//Reset the finished boolean
    	hole_finished = false;
    		
    	//Reset the stroke Counter
    	strokes = 0;
    }
}