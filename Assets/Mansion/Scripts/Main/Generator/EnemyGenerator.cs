﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EnemyGenerator : MonoBehaviour {
	public GameObject[] enemyPrefabArray;
	public MainController mainController;
	public UIGrid homePanelGrid;
	public UISprite exSprite;
	private List<Transform> mHomeChildList;
	private float mGenerateIntervalTime;
	private static EnemyGenerator sInstance;
	private List<EnemyData> mEnemyDataList;

	void Start () {
		sInstance = this;
		mHomeChildList = homePanelGrid.GetChildList ();
		mEnemyDataList = EnemyDataDao.Instance.QueryEnemyDataList ();
		mHomeChildList.Reverse ();
		SetGenerateIntervalTime ();
	}

	void Update () {
		mGenerateIntervalTime -= Time.deltaTime;
		if (mGenerateIntervalTime > 0) {
			return;
		}

		// アンロックされている部屋がなければ処理を終了
		List<RoomData> unlockRoomDataList = RoomDataDao.Instance.GetUnLockRoomDataList ();
		if (unlockRoomDataList.Count == 0) {
			Debug.Log ("return generate");
			Debug.Log ("not unlock room");
			SetGenerateIntervalTime ();
			return;
		}

		//泥棒が存在していなければ生成
		GameObject enemyObject = GameObject.FindWithTag ("Enemy");
		if(enemyObject != null){
			Debug.Log ("enemy exist");
			Debug.Log ("return generate");
			SetGenerateIntervalTime ();
			return;
		}
		GenerateEnemy (unlockRoomDataList);
	}

	void OnApplicationPause (bool pauseStatus) {
		if (pauseStatus) {
			DateTime dtNow = DateTime.Now;
			PrefsManager.Instance.SaveExitDate (dtNow.ToString ());
		} else {
			DateTime dtOld = DateTime.Parse (PrefsManager.Instance.GetExitDate ());
			DateTime dtNow = DateTime.Now;
			TimeSpan ts = dtNow - dtOld;
			//	int sleepHours = ts.Hours;
			int sleepHours = ts.Minutes;
			Debug.Log ("sleep Hours = " + sleepHours);
			if (sleepHours <= 0) {
				return;
			}
			GenerateEnemyWhileSleepTime (sleepHours);
		}
	}

	public static EnemyGenerator Instance {
		get {
			return sInstance;
		}
	}

	public void AttackedEnemy () {
		exSprite.enabled = false;
	}

	private void GenerateEnemy (List<RoomData> unlockRoomDataList) {
		Debug.Log ("generate enemy");

		//解放しているレベルによって出現させる泥棒を変更
		int decreaseCount = GetDecreaseCount (unlockRoomDataList);
		int enemyIndex = UnityEngine.Random.Range (0, enemyPrefabArray.Length - decreaseCount);
		int unlockRoomDataIndex = UnityEngine.Random.Range (0, unlockRoomDataList.Count);
		Debug.Log ("unlock size = " + unlockRoomDataList.Count);

		RoomData unlockRoomData = unlockRoomDataList [unlockRoomDataIndex];
		GameObject unlockRoomObject = mHomeChildList [unlockRoomData.Id].gameObject;
		GameObject enemyObject = Instantiate (enemyPrefabArray [enemyIndex]) as GameObject;
		enemyObject.BroadcastMessage ("SetEnemyData", mEnemyDataList [enemyIndex]);
		enemyObject.transform.parent = unlockRoomObject.transform;
		enemyObject.transform.localScale = new Vector3 (1, 1, 1);
		enemyObject.transform.localPosition = new Vector3 (0, 0, 0);
		unlockRoomObject.BroadcastMessage ("EnemyGenerated");

		//現在の画面がホームでなければ、生成した泥棒を非表示にする
		if (!mainController.CheckCurrentIsHomePanel ()) {
			enemyObject.BroadcastMessage ("Hide");
		}
		Debug.Log ("generated roomId = " + unlockRoomData.Id);

		exSprite.enabled = true;
		SetGenerateIntervalTime ();
		StatusDataKeeper.Instance.IncrementCameEnemyCount ();
		SoundManager.Instance.StopBGM ();
		SoundManager.Instance.PlayBGM (AudioClipID.BGM_ENEMY);
	}

	private void GenerateEnemyWhileSleepTime (int sleepHours) {
		SecomData secomdata = PrefsManager.Instance.GetSecomData ();
		List<RoomData> unlockRoomDataList = RoomDataDao.Instance.GetUnLockRoomDataList ();
		//解放しているレベルによって出現させる泥棒を変更
		int decreaseCount = GetDecreaseCount (unlockRoomDataList);
		DateTime dtNow = DateTime.Now;
		Debug.Log ("secom count = " + secomdata.Count);
		//セコムを持っていればセコムで撃退して履歴をセーブ
		for (int i = 0; i < secomdata.Count; i++) {
			int enemyId = UnityEngine.Random.Range (1, enemyPrefabArray.Length + 1 - decreaseCount);
			HistoryData historyData = new HistoryData ();
			historyData.EnemyId = enemyId;
			historyData.FlagSecom = 1;
			historyData.Damage = "0";
			historyData.Date = dtNow.AddHours (-sleepHours).ToString ("MM/dd HH:mm");
			secomdata.Count--;
			sleepHours--;
			HistoryDataDao.Instance.InsertHistoryData (historyData);
			PrefsManager.Instance.SaveSecomData (secomdata);
			//スリープ時間が０になったら処理を終了
			if (sleepHours <= 0) {
				return;
			}
		}

		List<EnemyData> enemyDataList = EnemyDataDao.Instance.QueryEnemyDataList ();
		int j = 1;
		//セコムで撃退しきれなかった分を減算
		for (int i = 0; i < sleepHours; i++) {
			int enemyId = UnityEngine.Random.Range (1, enemyPrefabArray.Length + 1 - decreaseCount);
			EnemyData enemyData = enemyDataList [enemyId - 1];
			decimal persent = CountManager.Instance.KeepMoneyCount / 100;
			decimal damage = enemyData.Atack * persent;
			damage = Math.Round (damage, 0, MidpointRounding.AwayFromZero);
			HistoryData historyData = new HistoryData ();
			historyData.EnemyId = enemyId;
			historyData.FlagSecom = 0;
			historyData.Damage = damage.ToString ();
			historyData.Date = dtNow.AddHours (-j).ToString ("MM/dd HH:mm");
			HistoryDataDao.Instance.InsertHistoryData (historyData);
			CountManager.Instance.DecreaseMoneyCount (damage);
			j++;
		}
	}

	private void SetGenerateIntervalTime () {
		mGenerateIntervalTime = 30.0f;
	}

	private int GetDecreaseCount (List<RoomData> unlockRoomDataList) {
		//解放しているレベルによって出現させる泥棒を変更
		int decreaseCount = 2;
		foreach (RoomData roomData in unlockRoomDataList) {
			if (roomData.Id >= 7) {
				decreaseCount = 1;
			}
			if (roomData.Id >= 8) {
				decreaseCount = 0;
			}
		}
		return decreaseCount;
	}
}
