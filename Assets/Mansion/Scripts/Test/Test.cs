using UnityEngine;
using System;
using System.Collections;

public class Test : MonoBehaviour {

	public GameObject a;

	// Use this for initialization
	void Start () {
		GameObject o =  Instantiate(a) as GameObject;
		o.transform.parent = gameObject.transform.parent;
	}
	
}
