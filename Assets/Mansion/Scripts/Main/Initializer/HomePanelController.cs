using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HomePanelController : MonoBehaviour {
	public UIGrid grid;
	public UICenterOnChild centerOnChild;
	private Transform mTargetChildTransform;
	private bool mCenterd;
	private GameObject mPitObject;
	private List<Transform> mChildList;

	void Awake () {
		mChildList = grid.GetChildList ();
		mPitObject = mChildList [11].gameObject;
	}

	public void Init () {
		if (centerOnChild.enabled) {
			centerOnChild.enabled = false;
		}
		mCenterd = false;
		centerOnChild.onCenter = OnCenter;
		centerOnChild.springStrength = 100.0f;
		List<RoomData> roomDataList = RoomDataDao.Instance.GetRoomDataList ();
		roomDataList.Reverse ();
		for (int i = 10; i >= 0; i--) {
			GameObject childObject = mChildList [i].gameObject;
			RoomData roomData = roomDataList [i];
			childObject.BroadcastMessage ("Init", roomData);
		}
		ShopItemData pitData = ShopItemDataDao.Instance.GetPitData ();
		mPitObject.BroadcastMessage ("Init", pitData);
		for (int i = 0; i < roomDataList.Count; i++) {
			RoomData roomData = roomDataList [i];
			if (roomData.ItemCount != 0) {
				mTargetChildTransform = mChildList [i];
				//神の国をセンターにするとずれるので１つ下げる
				if (i == 0) {
					mTargetChildTransform = mChildList [1];
				}
				//最初は１番下から始める
				centerOnChild.CenterOn (mChildList [10]);
				return;
			}
		}
		//最初は１番下から始める
		centerOnChild.CenterOn (mChildList [10]);
	}

	public void HideRoomObjects () {
		foreach (Transform roomTransform in mChildList) {
			roomTransform.gameObject.BroadcastMessage ("Hide");
		}
	}

	private void OnCenter (GameObject a) {
		if (mCenterd) {
			return;
		}
		mCenterd = true;
		StartCoroutine (reset ());
	}

	private IEnumerator reset () {
		yield return new WaitForSeconds (0.1f);
		if (mTargetChildTransform != null) {
			centerOnChild.springStrength = 8.0f;
			centerOnChild.CenterOn (mTargetChildTransform);
		}

	}
}
