using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Mouse Orbit with zoom")]
public class OrbitCamera : MonoBehaviour 
{
	public Transform target;
	public float distance = 5.0f;
	public float xSpeed = 120.0f;
	public float ySpeed = 120.0f;

	public float yMinLimit = -20f;
	public float yMaxLimit = 80f;

	public float distanceMin = 2.0f;
	public float distanceMax = 15f;

	private Rigidbody rigidbody;

	float x = 0.0f;
	float y = 0.0f;

	// Use this for initialization
	void Start () 
	{
		Vector3 angles = transform.eulerAngles;
		x = angles.y;
		y = angles.x;

		rigidbody = GetComponent<Rigidbody>();

		// Make the rigid body not change rotation
		if (rigidbody != null)
		{
			rigidbody.freezeRotation = true;
		}
	}

	void LateUpdate () 
	{
		//Debug.DrawRay(target.transform.position, getPlanarForward(this.gameObject), Color.green, 4, false);
		//Debug.DrawLine(this.transform.position, target.transform.position, Color.red);

		Ray ray = new Ray(this.transform.position, this.transform.forward);

		RaycastHit hit2;

		if(Physics.Linecast(this.transform.position, target.transform.position, out hit2))
		{
			Vector3 Direction = Quaternion.AngleAxis(getHitAngle(hit2.normal, ray), transform.forward) * transform.forward;

			//Debug.Log("Angle :" + getHitAngle(hit2.normal, ray));
			//Debug.DrawRay(target.transform.position, Direction, Color.blue, 4, false);
		}

		if (target) 
		{
			x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
			y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

			y = ClampAngle(y, yMinLimit, yMaxLimit);

			Quaternion rotation = Quaternion.Euler(y, x, 0);

			distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel")*5, distanceMin, distanceMax);

			RaycastHit hit;
			if (Physics.Linecast(target.position, transform.position, out hit))
			{
					distance -= hit.distance;
				Debug.Log(getHitAngle(hit.normal, ray));
			}

			Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
			Vector3 position = rotation * negDistance + target.position;


			//Slerp the camera movement so that we have a smooth transition between positions and rotations
			transform.rotation = Quaternion.Slerp(transform.rotation,rotation,.7f);
			transform.position = position;
		}
	}

	public static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360F)
			angle += 360F;
		if (angle > 360F)
			angle -= 360F;
		return Mathf.Clamp(angle, min, max);
	}

	public static Vector3 getPlanarForward(GameObject t)
	{
		//Get the direction of the camera
		Vector3 MoveDirection = t.transform.forward;

		//Make it planar
		MoveDirection.y = 0.0f;

		return Vector3.Normalize(MoveDirection);
	}

	public static float getHitAngle(Vector3 normal,Ray ray)
	{
		return Vector3.Angle(normal, ray.origin);

	}
}
