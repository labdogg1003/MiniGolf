using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour
{
	public Transform target;
	public GameObject camera;
	private bool shootBall = false;
	private float power = 0.0f;
	public float PowerMultiplier = 700f;
	private bool countup = true;
	private int zeroTime = 0;
	private float distToGround = 0;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () 
	{
		Debug.DrawRay(target.transform.position, getPlanarForward(this.gameObject), Color.green, 4, false);

		distToGround = target.GetComponent<Collider>().bounds.extents.y;

		if (!Input.GetMouseButton(0)) 
		{
			zeroTime = 0;
		}
		
		if (zeroTime < 1) 
		{
			if (Input.GetMouseButton(0)) {
				//power up
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
		

			if (Input.GetMouseButtonDown(0)) {
				//reset power
				resetPower();

				//tell the game we are ready to shoot
				shootBall = true;
			}
			if (Input.GetMouseButtonUp(0) && shootBall == true) {
				target.GetComponent<Rigidbody>().AddForce(getPlanarForward(camera) * (power * PowerMultiplier));

				//stop shooting the ball
				shootBall = false;

				resetPower();
			}

			if (Input.GetKeyDown("space") && IsGrounded()) 
			{
				Debug.Log("Jump");
				target.GetComponent<Rigidbody>().AddForce(Vector3.up * (1000));
			}
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
		Debug.Log(target.GetComponent<Rigidbody>().velocity.x + target.GetComponent<Rigidbody>().velocity.y + target.GetComponent<Rigidbody>().velocity.z);
		if ((target.GetComponent<Rigidbody>().velocity.x + target.GetComponent<Rigidbody>().velocity.y + target.GetComponent<Rigidbody>().velocity.z) > 0.1f) {
			if ((target.GetComponent<Rigidbody>().velocity.x + target.GetComponent<Rigidbody>().velocity.y + target.GetComponent<Rigidbody>().velocity.z) < 1.2) {
			//	target.GetComponent<Rigidbody>().drag += .0001f;
			}
		} else {
			//target.GetComponent<Rigidbody>().drag = 0f;
		}
	}

	public bool IsGrounded()
	{
		return Physics.Raycast(target.transform.position, -Vector3.up, distToGround + 0.3f);
	}
}
