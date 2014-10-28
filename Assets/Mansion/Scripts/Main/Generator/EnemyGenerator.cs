using UnityEngine;
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
		GenerateEnemyWhileSleepTime ();
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
		if (enemyObject != null) {
			Debug.Log ("enemy exist");
			Debug.Log ("return generate");
			SetGenerateIntervalTime ();
			return;
		}
		GenerateEnemy (unlockRoomDataList);
	}

	void OnApplicationPause (bool pauseStatus) {
		if (!pauseStatus) {
			GenerateEnemyWhileSleepTime ();
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

	private void GenerateEnemyWhileSleepTime () {
		Debug.Log ("GenerateEnemyWhileSleepTime");
		SecomData secomdata = PrefsManager.Instance.GetSecomData ();
		List<RoomData> unlockRoomDataList = RoomDataDao.Instance.GetUnLockRoomDataList ();
		string[] notificationDateArray = PrefsManager.Instance.NotificationDateArray;
		//初回起動時は中断データなナシ
		if (notificationDateArray.Length == 0) {
			return;
		}

		//解放しているレベルによって出現させる泥棒を変更
		int decreaseCount = GetDecreaseCount (unlockRoomDataList);
		Debug.Log ("date = " + notificationDateArray [0]);
		DateTime dtNow = DateTime.Now;
		Debug.Log ("secom count = " + secomdata.Count);
		List<EnemyData> enemyDataList = EnemyDataDao.Instance.QueryEnemyDataList ();
		foreach (string notificationDate in notificationDateArray) {
			DateTime dtNotification = DateTime.Parse (notificationDate);
			if (dtNotification > dtNow) {
				//dtNotificationはNowよりも新しい
				Debug.Log ("break");
				break;
			}

			int enemyId = UnityEngine.Random.Range (1, enemyPrefabArray.Length + 1 - decreaseCount);
			HistoryData historyData = new HistoryData ();
			historyData.EnemyId = enemyId;
			historyData.Date = notificationDate;
			//セコムで撃退
			if (secomdata.Count > 0) {
				historyData.FlagSecom = 1;
				historyData.Damage = "0";
				secomdata.Count--;
				StatusDataKeeper.Instance.IncrementUseSecomCount ();
				PrefsManager.Instance.SaveSecomData (secomdata);
				Debug.Log ("secom count = " + secomdata.Count);
			} else {
				EnemyData enemyData = enemyDataList [enemyId - 1];
				decimal persent = CountManager.Instance.KeepMoneyCount / 100;
				decimal damage = enemyData.Atack * persent;
				damage = Math.Round (damage, 0, MidpointRounding.AwayFromZero);
				historyData.FlagSecom = 0;
				historyData.Damage = damage.ToString ();
				CountManager.Instance.DecreaseMoneyCount (damage);
			}

			HistoryDataDao.Instance.InsertHistoryData (historyData);
			Debug.Log ("insert");
		}
		//ShopPanelInitializer.Instance.SetSecomLabel ();
		//StatusPanelInitializer.Instance.InitHistoryGrid ();
	}

	private void SetGenerateIntervalTime () {
		mGenerateIntervalTime = 45.0f;
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
