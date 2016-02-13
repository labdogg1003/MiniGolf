using UnityEngine;
using System.Collections;

public class RotateAround : MonoBehaviour 
{
	public int speed = 20;
	public bool x;
	public bool y;
	public bool z;

	void Update()
	{
		if (x) {
			transform.RotateAround(this.transform.position, Vector3.right, speed * Time.deltaTime);
		}
		if (y) {
			transform.RotateAround(this.transform.position, Vector3.forward, speed * Time.deltaTime);
		}
		if (z) {
			transform.RotateAround(this.transform.position, Vector3.up, speed * Time.deltaTime);
		}
	}
}
