using UnityEngine;
using System.Collections;

public class HoleEnd : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnTriggerEnter(Collider col)
	{
		//check that it was a ball that hit the trigger
		if(col.tag == "ball")
		{
			Debug.Log("finished");
			//Tell the player that they finished the hole;
			col.gameObject.GetComponentInParent<player>().hole_finished = true;
		}
	}
}
