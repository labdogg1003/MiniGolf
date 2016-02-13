using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	//This Might Change to the game manager handling the camera
	public Transform camera;
		
	public Transform cameraFocus;
	public GameObject ball;
		
	//Holds the score for the player -- ArrayList to allow flexibility on hole count
	public ArrayList Score;
		
	private bool hole_finished;
			
	//Holds the strokes for the hole, reset at the start of each hole
	private strokes = 0; 
		
	//Allows the player to hold 2 power-ups at a time.
	public powerUp[2] PowerUps;
		
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
    void addPowerUp(powerUp p)
    {
      if(powerUp[0] == null)
      {
          powerUp[0] = p;
      }
      else
      {
        powerUp[1] = powerUp[0];
        powerUp[0] = p;
      }
    }
    
    void usePowerUp()
    {
      if(powerUp[0] == null)
      {
      }
      else
      {
        powerUp[1] = null;
        powerUp[0] = powerUp[1];
      }
    }
    
    
	// This method adds the stroke amount to the score list
	// sets strokes back to zero and increments the hole number
    void holeReset()
    {
    	//add our final stroke count to the scorecard
    	Score.add(strokes)
    	
    	//Reset the finished boolean
    	finishedHole = false;
    		
    	//Reset the stroke Counter
    	Strokes = 0;
    }
}