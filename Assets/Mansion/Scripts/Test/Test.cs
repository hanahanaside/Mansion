using UnityEngine;
using System;
using System.Collections;

public class Test : MonoBehaviour {



	// Use this for initialization
	void Start () {

		#if UNITY_IPHONE
		Debug.Log("iPhone");
#elif UNITY_EDITOR
		Debug.Log("editor");
#endif


	}
	
}
