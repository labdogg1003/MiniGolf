using UnityEngine;
using System.Collections;

public class MoviePlayer : MonoBehaviour {

	public MovieTexture myMovie;

	// Use this for initialization
	void Start () 
	{
		myMovie.Play();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
