using UnityEngine;
using System.Collections;

public class StatusDataKeeper : MonoBehaviour {

	private static StatusDataKeeper sInstance;
	private StatusData mStatusData;

	void Start () {
		sInstance = this;
		mStatusData = PrefsManager.Instance.GetStatusData ();
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

	public void IncrementTotalGenerateCount () {
		mStatusData.TotalGenerateCount ++;
	}
	
	public void IncrementTotalTapPitCount () {
		mStatusData.TotalTapPitCount++;
	}

	public void AddTotalPitGenerateCount (int addCount) {
		mStatusData.TotalPitGenerateCount += addCount;
	}

}
