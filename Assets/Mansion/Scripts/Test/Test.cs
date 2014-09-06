using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Test : MonoBehaviour {


	void Start() {
		#if UNITY_IPHONE_API
		Debug.Log("aaaaaa");
		#endif

	}
}
