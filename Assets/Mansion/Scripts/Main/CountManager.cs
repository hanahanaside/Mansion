using UnityEngine;
using System.Collections;

public class CountManager : MonoBehaviour {

	public UILabel countLabel;
	public UILabel perSecondLabel;
	private static CountManager sInstance;
	private int mCount;
	private float mInterval = 1.0f;
	private float mTime;

	void Start () {
		if (sInstance == null) {
			sInstance = this;
		}
		Reset ();
	}

	void Update () {
		mTime -= Time.deltaTime;
		if (mTime <= 0.0f) {
			mCount++;
			countLabel.text = "count = " + mCount;
			Reset ();
		}
	}

	public static CountManager Instance {
		get {
			return sInstance;
		}
	}

	public void AddCount(int count){
		mCount += count;
		countLabel.text = "count = " + mCount;
	}

	private void Reset () {
		mTime = mInterval;
	}
}
