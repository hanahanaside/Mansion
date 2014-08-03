using UnityEngine;
using System.Collections;

public class HumanController : MonoBehaviour {

	private float walkSpeed = 0.005f;

	// Update is called once per frame
	void Update () {
		float x = transform.localPosition.x;
		if (x < -280) {
			transform.eulerAngles = new Vector3(0,0,0);
		}
		if (x > 280) {
			transform.eulerAngles = new Vector3(0,180,0);
		}
		transform.Translate (walkSpeed, 0, 0);
	}
}
