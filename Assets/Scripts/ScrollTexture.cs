using UnityEngine;
using System.Collections;

public class ScrollTexture : MonoBehaviour {
	float offset;
	Material sMaterial;
	public float offsetMultiplier; //whatever number works to make it match, dumb way of doing it probably.
	public float speed;
	void Awake () {
		sMaterial = this.GetComponent<Renderer>().material;
		offset = 0f;
	}

	void Update () {
		offset += (speed * offsetMultiplier * Time.deltaTime);
		sMaterial.SetTextureOffset("_MainTex", new Vector2(offset, 0));
	}
}

