using UnityEngine;
using System.Collections;
using System;

public class TutorialController : MonoBehaviour {
	void Start () {
		#if UNITY_EDITOR
		InsertHistoryData ();
		Application.LoadLevel ("Main");
		#endif

		#if UNITY_IPHONE
		SoundManager.Instance.PlayBGM (AudioClipID.BGM_MAIN);
		#endif

		#if UNITY_ANDROID
		PrefsManager.Instance.FlagTutorialFinished = 1;
		InsertHistoryData();
		Application.LoadLevel ("Main");
		#endif

	}

	private void InsertHistoryData () {
		DateTime dtNow = DateTime.Now;
		string date = dtNow.ToString ("MM/dd HH:mm");
		HistoryData historyData = new HistoryData ();
		historyData.EnemyId = 2;
		historyData.Damage = "31";
		historyData.Date = date;
		historyData.FlagSecom = 0;
		HistoryDataDao.Instance.InsertHistoryData (historyData);
	}
}
