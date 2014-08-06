using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CountManager : MonoBehaviour {

	public UILabel generatedCountLabel;
	public UILabel totalGenerateSpeedLabel;
	private static CountManager sInstance;
	private int mGeneratedCount;
	private float mTotalGenerateSpeed;
	private float mTime;

	void Start () {
		sInstance = this;
		mTotalGenerateSpeed = RoomDataDao.Instance.GetTotalGenerateSpeed ();
		totalGenerateSpeedLabel.text = mTotalGenerateSpeed + " / \u79d2";
		ResetTime ();
	}

	void Update () {
		mTime -= Time.deltaTime;
		if (mTime <= 0.0f) {
			mGeneratedCount++;
			generatedCountLabel.text = "count = " + mGeneratedCount;
			ResetTime ();
		}
	}

	public static CountManager Instance {
		get {
			return sInstance;
		}
	}

	public void AddGeneratedCount (int addCount) {
		mGeneratedCount += addCount;
		generatedCountLabel.text = "count = " + mGeneratedCount;
	}

	public void AddGenerateSpeed (float addSpeed) {
		mTotalGenerateSpeed += addSpeed;
		totalGenerateSpeedLabel.text = mTotalGenerateSpeed + " / \u79d2";
	}

	private void ResetTime () {
		mTime = 1.0f / mTotalGenerateSpeed;
	}
}
