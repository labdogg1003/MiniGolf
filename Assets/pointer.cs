using UnityEngine;
using System.Collections;

public class pointer : MonoBehaviour 
{
	public Camera mainCamera;
	private LineRenderer line;
	private Shoot shoot;
	public float maxDistance = 2;
	public float distance;

	// Use this for initialization
	void Start () 
	{
		line = this.GetComponent<LineRenderer>();
		shoot = mainCamera.GetComponent<Shoot>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Debug.Log("current Player" + game.currentPlayer.name);
		if(!shoot.hasBeenHit)
		{
			line.enabled = true;
			line.SetPosition(0,Game.Instance.currentPlayer.ball.transform.position);

			distance = maxDistance;
			checkDistance();
			line.SetPosition(1,Game.Instance.currentPlayer.ball.transform.position + getPlanarForward(mainCamera.gameObject) * distance);
		}
		else
		{
			line.enabled = false;
		}
	}

	public static Vector3 getPlanarForward(GameObject t)
	{
		//Get the direction of the camera
		Vector3 MoveDirection = t.transform.forward;

		//Make it planar
		MoveDirection.y = 0.0f;

		return Vector3.Normalize(MoveDirection);
	}

	public void checkDistance()
	{
		RaycastHit hit;
		if(Physics.Raycast(Game.Instance.currentPlayer.ball.transform.position, 
			getPlanarForward(mainCamera.gameObject),out hit, maxDistance))
		{
			distance = hit.distance;
		}
	}

}
