using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyGenerator : MonoBehaviour {

	public GameObject[] enemyPrefabArray;
	public UIGrid homePanelGrid;
	private List<Transform> mHomeChildList;
	private float mGenerateIntervalTime;

	void Start () {
		mHomeChildList = homePanelGrid.GetChildList ();
		mHomeChildList.Reverse ();
		SetGenerateIntervalTime ();
	}

	void Update () {
		mGenerateIntervalTime -= Time.deltaTime;
		if (mGenerateIntervalTime < 0) {
			GenerateEnemy ();
		}
	}

	private void GenerateEnemy () {
		GameObject enemyObject = GameObject.FindWithTag ("Enemy");
		if (enemyObject != null) {
			Debug.Log("return generate");
			SetGenerateIntervalTime();
			return;
		}
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
		enemyObject = Instantiate (enemyPrefabArray [enemyIndex]) as GameObject;
		enemyObject.transform.parent = unlockRoomObject.transform;
		enemyObject.transform.localScale = new Vector3 (1, 1, 1);
		enemyObject.transform.localPosition = new Vector3 (0, -100, 0);
		Debug.Log("generated roomId = "+unlockRoomData.Id);
	}

	private void SetGenerateIntervalTime () {
		mGenerateIntervalTime = 5.0f;
	}
}
