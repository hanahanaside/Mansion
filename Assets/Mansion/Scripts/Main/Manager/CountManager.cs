using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CountManager : MonoBehaviour {
	public UILabel keepMoneyCountLabel;
	public UILabel totalGenerateSpeedLabel;
	public iTweenEvent boostEvent;
	private static CountManager sInstance;
	private long mKeepMoneyCount;
	private double mTotalGenerateSpeed;
	private double mTime;
	private int mBoostPower = 1;

	void Start () {
		sInstance = this;
		UpdateGenerateSpeed ();
		mKeepMoneyCount = PrefsManager.Instance.GetMoneyCount ();
		Debug.Log ("count = " + mKeepMoneyCount);
		SetKeepCountLabel ();
		if (mTotalGenerateSpeed != 0) {
			ResetTime ();
		}
	}

	void Update () {
		if (mTotalGenerateSpeed == 0) {
			// frist launch
			return;
		}
		mTime -= Time.deltaTime * mBoostPower;
		if (mTime < 0.0f) {
			mKeepMoneyCount++;
			SetKeepCountLabel ();
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

	public void AddMoneyCount (long addCount) {
		mKeepMoneyCount += addCount;
		SetKeepCountLabel ();
	}

	public void AddGenerateSpeed(double addSpeed){
		mTotalGenerateSpeed += addSpeed;
		totalGenerateSpeedLabel.text = Math.Round (mTotalGenerateSpeed, 1, MidpointRounding.AwayFromZero) + "/\u79d2";
		ResetTime ();
	}

	public void UpdateGenerateSpeed () {
		mTotalGenerateSpeed = RoomDataDao.Instance.GetTotalGenerateSpeed ();
		totalGenerateSpeedLabel.text = Math.Round (mTotalGenerateSpeed, 1, MidpointRounding.AwayFromZero) + " / \u79d2";
	}

	public void DecreaseMoneyCount (long decreaseCount) {
		mKeepMoneyCount -= decreaseCount;
		SetKeepCountLabel ();
	}

	public void StartBoost () {
		mBoostPower = 3;
		totalGenerateSpeedLabel.color = new Color (1f, 0.7f, 0.016f, 1f);
		boostEvent.Play ();
	}

	public void StopBoost () {
		mBoostPower = 1;
		boostEvent.Stop ();
		totalGenerateSpeedLabel.transform.localScale = new Vector3 (1, 1, 1);
		totalGenerateSpeedLabel.color = Color.white;
	}

	public long KeepMoneyCount {
		get {
			return mKeepMoneyCount;
		}
	}

	private void ResetTime () {
		mTime = 1.0f / mTotalGenerateSpeed;
	}

	private void SetKeepCountLabel () {
		//	keepMoneyCountLabel.text = mKeepMoneyCount + " \u5186";
		keepMoneyCountLabel.text = mKeepMoneyCount + "";
	}
}
