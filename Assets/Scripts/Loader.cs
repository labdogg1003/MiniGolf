using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour 
{
	public GameObject gameManager;			//GameManager prefab to instantiate.
	public GameObject soundManager;			//SoundManager prefab to instantiate.


	void Update ()
	{
		//Check if a GameManager has already been assigned to static variable GameManager.instance or if it's still null
		if (Game.Instance == null)

			//Instantiate gameManager prefab
			Instantiate(gameManager);

		//Check if a SoundManager has already been assigned to static variable GameManager.instance or if it's still null
		if (AudioManager.Instance == null)

			//Instantiate SoundManager prefab
			Instantiate(soundManager);
	}
}
