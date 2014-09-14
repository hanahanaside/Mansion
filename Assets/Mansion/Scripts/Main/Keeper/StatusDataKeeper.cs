using UnityEngine;
using System.Collections;
using System;

public class StatusDataKeeper : MonoBehaviour {

	private static StatusDataKeeper sInstance;
	private StatusData mStatusData;

	void Start () {
		sInstance = this;
		mStatusData = PrefsManager.Instance.GetStatusData ();
	}

	void Update(){
		decimal currentKeepCount = CountManager.Instance.KeepMoneyCount;
		if(currentKeepCount > mStatusData.MaxKeepCount){
			mStatusData.MaxKeepCount = currentKeepCount;
		}
	}

	void OnApplicationPause (bool pauseStatus) {
		if (pauseStatus) {
			PrefsManager.Instance.SaveStatusData (mStatusData);
		}
	}

	public static StatusDataKeeper Instance {
		get {
			return sInstance;
		}
	}

	public StatusData StatusData {
		get {
			return mStatusData;
		}
	}

	public void AddTotalGenerateCount (decimal addCount) {
		mStatusData.TotalGenerateCount += addCount;
	}
	
	public void IncrementTotalTapPitCount () {
		if(String.IsNullOrEmpty(mStatusData.FirstGenerateDate)){
			DateTime dtNow = DateTime.Now;
			mStatusData.FirstGenerateDate = dtNow.ToString ();
		}
		mStatusData.TotalTapPitCount++;
	}

	public void AddTotalPitGenerateCount (int addCount) {
		mStatusData.TotalPitGenerateCount += addCount;
	}

	public void IncrementCameEnemyCount(){
		mStatusData.TotalCameEnemyCount++;
	}

	public void IncrementAtackEnemyCount(){
		mStatusData.TotalAtackEnemyCount++;
	}

	public void IncrementUseSecomCount(){
		mStatusData.TotalUsedSecomCount++;
	}

	public void AddDamagedCount(decimal damagedCount){
		mStatusData.TotalDamegedCount += damagedCount;
	}
}
