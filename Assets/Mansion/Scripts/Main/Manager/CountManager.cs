using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

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
		totalGenerateSpeedLabel.text = Math.Round((double)mTotalGenerateSpeed, 1, MidpointRounding.AwayFromZero) + " / \u79d2";
		mKeepMoneyCount = PrefsManager.Instance.GetMoneyCount ();
		keepMoneyCountLabel.text = "count = " + mKeepMoneyCount;
		if(mTotalGenerateSpeed != 0){
			ResetTime ();
		}
	}

	void Update () {
		if(mTotalGenerateSpeed == 0){
			// frist launch
			return;
		}
		mTime -= Time.deltaTime;
		if (mTime < 0.0f) {
			mKeepMoneyCount++;
			SetKeepCountLabel();
			StatusDataKeeper.Instance.IncrementTotalGenerateCount ();
			ResetTime ();
		}
	}

	void OnApplicationPause (bool pauseStatus) {
		if (pauseStatus) {
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
		SetKeepCountLabel();
	}

	public void AddGenerateSpeed (float addSpeed) {
		mTotalGenerateSpeed += addSpeed;
		totalGenerateSpeedLabel.text = Math.Round((double)mTotalGenerateSpeed, 1, MidpointRounding.AwayFromZero) + " / \u79d2";
		SetKeepCountLabel();
		ResetTime();
	}

	public void DecreaseMoneyCount (int decreaseCount) {
		mKeepMoneyCount -= decreaseCount;
	}

	public int KeepMoneyCount {
		get {
			return mKeepMoneyCount;
		}
	}

	private void ResetTime () {
		mTime = 1.0f / mTotalGenerateSpeed;
	}

	private void SetKeepCountLabel(){
		keepMoneyCountLabel.text = mKeepMoneyCount + "\u5186";
	}
}
