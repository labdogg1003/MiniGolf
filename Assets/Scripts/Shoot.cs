using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour
{
	//public Transform target;
	//public GameObject camera;
	public player currentPlayer;
	private bool shootBall = false;
	private float power = 0.0f;
	public float PowerMultiplier = 700f;
	private bool countup = true;
	private int zeroTime = 0;
	private float distToGround = 0;

	public bool hasBeenHit = false;
	public bool canShoot = true;


	// Use this for initialization
	void Start () 
	{
	
	}

	// Update is called once per frame
	void Update () 
	{
		//TODO: There has to be another way to do this
		currentPlayer = FindObjectOfType<Game>().currentPlayer;

		Debug.DrawRay(currentPlayer.ball.transform.position, getPlanarForward(currentPlayer.camera.gameObject), Color.green, 4, false);

		distToGround = currentPlayer.ball.GetComponent<Collider>().bounds.extents.y;

		if (!Input.GetMouseButton(0)) 
		{
			zeroTime = 0;
		}
		
		if (zeroTime < 1) 
		{
			if (Input.GetMouseButton(0)) 
			{
				if (power < 3.0f && countup) {
					power += Time.deltaTime * 2f;
				} else if (power < 0) {
					countup = true;
					resetPower();
					zeroTime++;
				} else {
					countup = false;
					power -= Time.deltaTime * 2f;
				}
					
			}
		

			if (Input.GetMouseButtonDown(0))
			{
				//reset power
				resetPower();

				//tell the game we are ready to shoot
				shootBall = true;
			}
			if (Input.GetMouseButtonUp(0) && shootBall == true && !hasBeenHit) {
				currentPlayer.ball.GetComponent<Rigidbody>().AddForce(getPlanarForward(currentPlayer.camera) * (power * PowerMultiplier));

				//stop shooting the ball
				shootBall = false;

				resetPower();

				//get our currentplayer and increment their strokes
				FindObjectOfType<Game>().currentPlayer.strokes++;

				//Dont Allow Them to Shoot Again
				hasBeenHit = true;
			}

			if (Input.GetKeyDown("space")) 
			{
				Debug.Log("Power Up : " 
					+ currentPlayer.PowerUps[0].powerUpType.ToString()
					+ " used by "
					+ currentPlayer.name);

				currentPlayer.usePowerUp();
			}
		}

		//Check that the ball has already been hit and that it is sleeping to end their turn
		if(hasBeenHit && currentPlayer.ball.GetComponent<Rigidbody>().IsSleeping())
		{
			
			changeCamera();

			FindObjectOfType<Game>().switchPlayer();
			Debug.Log("Turn Over Switching To Next Player");
		}

		float powerPerc = power / 3.0f;

	}

	public static Vector3 getPlanarForward(GameObject t)
	{
		//Get the direction of the camera
		Vector3 MoveDirection = t.transform.forward;

		//Make it planar
		MoveDirection.y = 0.0f;

		return Vector3.Normalize(MoveDirection);
	}

	public void resetPower()
	{
		this.power = 0.0f;
	}

	public void OnCollisionStay(Collision collisionInfo)
	{
		Debug.Log(currentPlayer.ball.GetComponent<Rigidbody>().velocity.x + currentPlayer.ball.GetComponent<Rigidbody>().velocity.y + currentPlayer.ball.GetComponent<Rigidbody>().velocity.z);
		if ((currentPlayer.ball.GetComponent<Rigidbody>().velocity.x + currentPlayer.ball.GetComponent<Rigidbody>().velocity.y + currentPlayer.ball.GetComponent<Rigidbody>().velocity.z) > 0.1f) {
			if ((currentPlayer.ball.GetComponent<Rigidbody>().velocity.x + currentPlayer.ball.GetComponent<Rigidbody>().velocity.y + currentPlayer.ball.GetComponent<Rigidbody>().velocity.z) < 1.2) {
			//	target.GetComponent<Rigidbody>().drag += .0001f;
			}
		} else {
			//target.GetComponent<Rigidbody>().drag = 0f;
		}
	}

	public void changeCamera()
	{
		
		currentPlayer.camera = null;
		Debug.Log(currentPlayer.camera == null);
	}

	public bool IsGrounded()
	{
		return Physics.Raycast(currentPlayer.ball.transform.position, -Vector3.up, distToGround + 0.3f);
	}
}
