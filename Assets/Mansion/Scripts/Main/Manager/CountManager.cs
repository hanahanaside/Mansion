using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CountManager : MonoBehaviour {

	public UILabel keepMoneyCountLabel;
	public UILabel totalGenerateSpeedLabel;
	private static CountManager sInstance;
	private long mKeepMoneyCount;
	private float mTotalGenerateSpeed;
	private float mTime;

	void Start () {
		sInstance = this;
		UpdateGenerateSpeed ();
		mKeepMoneyCount = PrefsManager.Instance.GetMoneyCount ();
		SetKeepCountLabel();
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

	public void AddMoneyCount (int addCount) {
		mKeepMoneyCount += addCount;
		SetKeepCountLabel();
	}

	public void UpdateGenerateSpeed(){
		mTotalGenerateSpeed = RoomDataDao.Instance.GetTotalGenerateSpeed ();
		totalGenerateSpeedLabel.text = Math.Round((double)mTotalGenerateSpeed, 1, MidpointRounding.AwayFromZero) + " / \u79d2";
	}

	public void DecreaseMoneyCount (long decreaseCount) {
		mKeepMoneyCount -= decreaseCount;
		SetKeepCountLabel();
	}

	public long KeepMoneyCount {
		get {
			return mKeepMoneyCount;
		}
	}

	private void ResetTime () {
		mTime = 1.0f / mTotalGenerateSpeed;
	}

	private void SetKeepCountLabel(){
		keepMoneyCountLabel.text = mKeepMoneyCount + " \u5186";
	}
}
