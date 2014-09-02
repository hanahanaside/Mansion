using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Test : MonoBehaviour {

	public UILabel label;

	void OnEnable () {
		label.color = Color.cyan;
		TwitterManager.tweetSheetCompletedEvent += tweetSheetCompletedEvent;
	}

	void OnDisable () {
		TwitterManager.tweetSheetCompletedEvent -= tweetSheetCompletedEvent;
	}

	void tweetSheetCompletedEvent (bool didSucceed) {
		Debug.Log ("tweetSheetCompletedEvent " + didSucceed);
		if (didSucceed) {

		}
	}


	public void OnButtonClicked(){
		TwitterBinding.showTweetComposer ("test");
	}
}
