using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CountManager : MonoBehaviour {
	public UILabel keepMoneyCountLabel;
	public UILabel totalGenerateSpeedLabel;
	public iTweenEvent boostEvent;
	private static CountManager sInstance;
	private decimal mKeepMoneyCount;
	private decimal mTotalGenerateSpeed;
	private double mTime;
	private int mBoostPower = 1;

	void Start () {
		sInstance = this;
		UpdateGenerateSpeed ();
		mKeepMoneyCount = PrefsManager.Instance.GetMoneyCount ();
		SetKeepCountLabel ();
		if (mTotalGenerateSpeed == 0) {
			return;
		}
		ResetTime ();
		if (!string.IsNullOrEmpty (PrefsManager.Instance.ExitDate)) {
			AddSleepGenerateCount ();
		}
	}

	void Update () {
		if (mTotalGenerateSpeed == 0) {
			// frist launch
			return;
		}
		mTime -= Time.deltaTime;
		if (mTime <= 0f) {
			decimal addCount = 0;
			#if UNITY_IPHONE
			decimal disCountPower = 30 / mBoostPower;
			addCount = mTotalGenerateSpeed / disCountPower;
			#endif
			#if UNITY_ANDROID
			decimal disCountPower = 55 / mBoostPower;
			addCount = mTotalGenerateSpeed / disCountPower;
			#endif
			mKeepMoneyCount += addCount;
			SetKeepCountLabel ();
			StatusDataKeeper.Instance.AddTotalGenerateCount (addCount);
			ResetTime ();
		}
	}

	void OnApplicationPause (bool pauseStatus) {
		if (pauseStatus) {
			PrefsManager.Instance.SaveMoneyCount (mKeepMoneyCount);
			string exitDate = DateTime.Now.ToString ();
			PrefsManager.Instance.ExitDate = exitDate;
		} else {
			AddSleepGenerateCount ();
		}
	}

	public static CountManager Instance {
		get {
			return sInstance;
		}
	}

	public void AddMoneyCount (decimal addCount) {
		mKeepMoneyCount += addCount;
		SetKeepCountLabel ();
	}

	public void AddGenerateSpeed (decimal addSpeed) {
		Debug.Log ("speed = " + addSpeed);
		mTotalGenerateSpeed += addSpeed;
		SetGenerateSpeedLabel ();
		ResetTime ();
	}

	public void UpdateGenerateSpeed () {
		decimal pitBonusTimes = PitDataKeeper.Instance.GetPitBonusTimes ();
		mTotalGenerateSpeed = RoomDataDao.Instance.GetTotalGenerateSpeed () * pitBonusTimes;
		SetGenerateSpeedLabel ();
	}

	public void DecreaseMoneyCount (decimal decreaseCount) {
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
		totalGenerateSpeedLabel.color = Color.black;
	}

	public decimal KeepMoneyCount {
		get {
			return mKeepMoneyCount;
		}
	}

	private void AddSleepGenerateCount () {
		string exitDate = PrefsManager.Instance.ExitDate;
		DateTime dtExit = DateTime.Parse (exitDate);
		DateTime dtNow = DateTime.Now;
		TimeSpan ts = dtNow - dtExit;
		int sleepSeconds = ts.Seconds;
		Debug.Log ("sleep seconds = " + sleepSeconds);
		for (int i = 0; i < sleepSeconds; i++) {
			AddMoneyCount (mTotalGenerateSpeed);
		}
	}

	private void ResetTime () {
		mTime = 0.01f;
		//mTime = 1f;
	}

	private void SetKeepCountLabel () {
		keepMoneyCountLabel.text = CommaMarker.MarkDecimalCount (mKeepMoneyCount) + " \u5186";
	}

	private void SetGenerateSpeedLabel () {
		totalGenerateSpeedLabel.text = CommaMarker.MarkGenerateSpeed (mTotalGenerateSpeed) + " / \u79d2";

	}
}
