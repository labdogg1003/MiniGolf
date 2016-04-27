﻿using UnityEngine;
using System.Collections;

public class CameraFocus : MonoBehaviour 
{
	public Transform target;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.transform.position = new Vector3(target.transform.position.x, target.transform.position.y + 2, target.transform.position.z);
	}
}
