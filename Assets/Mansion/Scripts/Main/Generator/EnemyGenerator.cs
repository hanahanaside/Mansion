using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EnemyGenerator : MonoBehaviour {
	public GameObject[] enemyPrefabArray;
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
		if (mGenerateIntervalTime < 0) {
			GenerateEnemy ();
		}
	}

	void OnApplicationPause (bool pauseStatus) {
		if (pauseStatus) {
			DateTime dtNow = DateTime.Now;
			PrefsManager.Instance.SaveExitDate (dtNow.ToString ());
		} else {
			DateTime dtOld = DateTime.Parse (PrefsManager.Instance.GetExitDate ());
			DateTime dtNow = DateTime.Now;
			TimeSpan ts = dtNow - dtOld;
			int sleepHours = ts.Hours;
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

	private bool IsEnemyExist {
		set;
		get;
	}

	public void AttackedEnemy () {
		exSprite.enabled = false;
		IsEnemyExist = false;
	}

	private void GenerateEnemy () {
		// enemy exist
		if (IsEnemyExist) {
			Debug.Log ("return generate");
			SetGenerateIntervalTime ();
			return;
		}

		// no more unlock room
		List<RoomData> unlockRoomDataList = RoomDataDao.Instance.GetUnLockRoomDataList ();
		if (unlockRoomDataList.Count == 0) {
			Debug.Log ("return generate");
			Debug.Log ("not unlock room");
			SetGenerateIntervalTime ();
			return;
		}
		Debug.Log ("generate enemy");

		int enemyIndex = UnityEngine.Random.Range (0, enemyPrefabArray.Length);
		int unlockRoomDataIndex = UnityEngine.Random.Range (0, unlockRoomDataList.Count);
		Debug.Log ("unlock size = " + unlockRoomDataList.Count);

		RoomData unlockRoomData = unlockRoomDataList [unlockRoomDataIndex];
		GameObject unlockRoomObject = mHomeChildList [unlockRoomData.Id].gameObject;
		GameObject enemyObject = Instantiate (enemyPrefabArray [enemyIndex]) as GameObject;
		enemyObject.BroadcastMessage ("SetEnemyData", mEnemyDataList [enemyIndex]);
		enemyObject.transform.parent = unlockRoomObject.transform;
		enemyObject.transform.localScale = new Vector3 (1, 1, 1);
		enemyObject.transform.localPosition = new Vector3 (0, 0, 0);

		Debug.Log ("generated roomId = " + unlockRoomData.Id);

		InsertHistoryData (mEnemyDataList [enemyIndex]);
		exSprite.enabled = true;
		SetGenerateIntervalTime ();
		StatusDataKeeper.Instance.IncrementCameEnemyCount ();
		IsEnemyExist = true;
		SoundManager.Instance.StopBGM ();
		SoundManager.Instance.PlayBGM (AudioClipID.BGM_ENEMY);
	}

	private void GenerateEnemyWhileSleepTime (int sleepHours) {
		int secomCount = PrefsManager.Instance.GetSecomData ().Count;
		//セコムを持っていればセコムで撃退して履歴をセーブ
		for (int i = 0; i < secomCount; i++) {
			secomCount--;
			sleepHours--;

			//スリープ時間が０になったら処理を終了
			if (sleepHours <= 0) {
				return;
			}
		}
		for (int i = 0; i < sleepHours; i++) {

		}
	}

	private void InsertHistoryData (EnemyData enemyData) {

	}

	private void SetGenerateIntervalTime () {
		mGenerateIntervalTime = 20.0f;
	}
}
