using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hole : MonoBehaviour 
{
	List<powerUp> powerUps = new List<powerUp>();
	public GameObject holePowerUps;
	public Vector3 startPoint;

	// Use this for initialization
	void Start () 
	{
		holePowerUps = this.gameObject.gameObject.transform.FindChild("HolePowerUps");

		//Grab all PowerUps in the level (note not hole)
		foreach (Transform child in holePowerUps.transform)
		{
			powerUps.Add(child.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
