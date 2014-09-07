using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Test : MonoBehaviour {


	void Start(){
		Hashtable hash = new Hashtable ();
		hash.Add ("x", 1000);
		hash.Add ("y",0);
		hash.Add ("time", 100);
		hash.Add ("islocal", true);
		//	hash.Add ("oncomplete", "OnMoveAnimationCompleted");
		hash.Add ("easetype", iTween.EaseType.linear);
		iTween.MoveAdd (gameObject, hash);

	}
}
