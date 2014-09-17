using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Test : MonoBehaviour {

	public UISprite sprite;

	void Start(){
		Vector3 position = sprite.gameObject.transform.position;
		Debug.Log ("p = " + position);
	}
}
