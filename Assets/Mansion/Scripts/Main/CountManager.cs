using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CountManager : MonoBehaviour {

	public UILabel keepMoneyCountLabel;
	public UILabel totalGenerateSpeedLabel;
	private static CountManager sInstance;
	private int mKeepMoneyCount;
	private float mTotalGenerateSpeed;
	private float mTime;

	void Start () {
		sInstance = this;
		mTotalGenerateSpeed = RoomDataDao.Instance.GetTotalGenerateSpeed ();
		totalGenerateSpeedLabel.text = mTotalGenerateSpeed + " / \u79d2";
		mKeepMoneyCount = PrefsManager.Instance.GetMoneyCount ();
		keepMoneyCountLabel.text = "count = " + mKeepMoneyCount;
		ResetTime ();
	}

	void Update () {
		mTime -= Time.deltaTime;
		if (mTime <= 0.0f) {
			mKeepMoneyCount++;
			keepMoneyCountLabel.text = "count = " + mKeepMoneyCount;
			StatusDataKeeper.Instance.IncrementTotalGenerateCount ();
			ResetTime ();
		}
	}

	void OnApplicationPause (bool pauseStatus) {
		if (pauseStatus) {
			Debug.Log("ppppppp");
			PrefsManager.Instance.SaveMoneyCount (mKeepMoneyCount);
		}
	}

	public static CountManager Instance {
		get {
			return sInstance;
		}
	}

	public void AddGeneratedCount (int addCount) {
		mKeepMoneyCount += addCount;
		keepMoneyCountLabel.text = "count = " + mKeepMoneyCount;
	}

	public void AddGenerateSpeed (float addSpeed) {
		mTotalGenerateSpeed += addSpeed;
		totalGenerateSpeedLabel.text = mTotalGenerateSpeed + " / \u79d2";
	}

	private void ResetTime () {
		mTime = 1.0f / mTotalGenerateSpeed;
	}
}
