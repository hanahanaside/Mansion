using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HomePanelController : MonoBehaviour {
	public UIGrid grid;
	public UICenterOnChild centerOnChild;
	public UIScrollView scrollView;
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
		mPitObject.BroadcastMessage ("Init");

		SetCenterPosition (roomDataList);
	}

	private void SetCenterPosition (List<RoomData> roomDataList) {
		//ドロボーが存在している場合はドロボーの部屋をセンターにセット
		GameObject enemyObject = GameObject.FindWithTag ("Enemy");
		if (enemyObject != null) {
			GameObject roomObject = enemyObject.transform.parent.gameObject;
			int index = grid.GetIndex (roomObject.transform);
			CenterOnChild (index);
			return;
		}
		for (int i = 0; i < roomDataList.Count; i++) {
			RoomData roomData = roomDataList [i];
			//ダンボールハウスしかアンロックしていない場合は何もしない
			if (i == 10) {
				break;
			}
			if (roomData.ItemCount != 0) {
				CenterOnChild (i);
				return;
			}
		}
		//最初は１番下から始める
		scrollView.transform.localPosition = new Vector3 (0, 3307, 0);

	}

	private void CenterOnChild (int index) {
		mTargetChildTransform = mChildList [index];
		//神の国をセンターにするとずれるので１つ下げる
		if (index == 0) {
			mTargetChildTransform = mChildList [1];
		}
		//スクロールを下からに見せるために一旦、一番下をセンターにセット
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
