using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
		mEnemyDataList = EnemyDataDao.Instance.QueryEnemyDataList();
		mHomeChildList.Reverse ();
		SetGenerateIntervalTime ();
	}

	void Update () {
		mGenerateIntervalTime -= Time.deltaTime;
		if (mGenerateIntervalTime < 0) {
			GenerateEnemy ();
		}
	}

	public static EnemyGenerator Instance{
		get{
			return sInstance;
		}
	}

	private bool IsEnemyExist{
		set;get;
	}

	public void AttackedEnemy(){
		exSprite.enabled = false;
		IsEnemyExist = false;
	}
	
	private void GenerateEnemy () {
		// enemy exist
		if(IsEnemyExist){
			Debug.Log("return generate");
			SetGenerateIntervalTime();
			return;
		}

		// no more unlock room
		List<RoomData> unlockRoomDataList = RoomDataDao.Instance.GetUnLockRoomDataList ();
		if(unlockRoomDataList.Count ==0){
			Debug.Log("return generate");
			Debug.Log("not unlock room");
			SetGenerateIntervalTime();
			return;
		}
		Debug.Log ("generate enemy");

		int enemyIndex = Random.Range (0, enemyPrefabArray.Length);
		int unlockRoomDataIndex = Random.Range (0, unlockRoomDataList.Count);
		Debug.Log("unlock size = "+unlockRoomDataList.Count);

		RoomData unlockRoomData = unlockRoomDataList [unlockRoomDataIndex];
		GameObject unlockRoomObject = mHomeChildList [unlockRoomData.Id].gameObject;
		GameObject enemyObject = Instantiate (enemyPrefabArray [enemyIndex]) as GameObject;
		enemyObject.BroadcastMessage("SetEnemyData", mEnemyDataList[enemyIndex]);
		enemyObject.transform.parent = unlockRoomObject.transform;
		enemyObject.transform.localScale = new Vector3 (1, 1, 1);
		enemyObject.transform.localPosition = new Vector3 (0, 0, 0);

		Debug.Log("generated roomId = "+unlockRoomData.Id);

		InsertHistoryData(mEnemyDataList[enemyIndex]);
		exSprite.enabled = true;
		SetGenerateIntervalTime();
		StatusDataKeeper.Instance.IncrementCameEnemyCount ();
		IsEnemyExist = true;
		SoundManager.Instance.StopBGM();
		SoundManager.Instance.PlayBGM(AudioClipID.BGM_ENEMY);
	}

	private void InsertHistoryData(EnemyData enemyData){

	}

	private void SetGenerateIntervalTime () {
		mGenerateIntervalTime = 20.0f;
	}
}
