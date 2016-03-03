using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hole : MonoBehaviour 
{
	List<GameObject> powerUps = new List<GameObject>();
	GameObject holePowerUps;
	public GameObject startPoint;
	public string holeName;

	// Use this for initialization
	void Start () 
	{
		holePowerUps = this.gameObject.transform.FindChild("HolePowerUps").gameObject;

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
