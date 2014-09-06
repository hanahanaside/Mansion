using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HomePanelInitializer : MonoBehaviour {
	public UIGrid grid;
	public UIScrollView scrollView;
	public GameObject pitPrefab;
	public UICenterOnChild centerOnChild;
	private Transform mTargetChildTransform;
	private bool mCenterd;

	public void Init () {
		if (centerOnChild.enabled) {
				centerOnChild.enabled = false;
		}
		mCenterd = false;
		centerOnChild.onCenter = Hoge;
		centerOnChild.springStrength = 100.0f;
		List<Transform> childList = grid.GetChildList ();
		List<RoomData> roomDataList = RoomDataDao.Instance.GetRoomDataList ();
		roomDataList.Reverse ();
		for (int i = 10; i >= 0; i--) {
			GameObject childObject = childList [i].gameObject;
			RoomData roomData = roomDataList [i];
			childObject.BroadcastMessage ("Init", roomData);
		}
		GameObject pitObject = childList [11].gameObject;
		ShopItemData pitData = ShopItemDataDao.Instance.GetPitData ();
		pitObject.BroadcastMessage ("Init", pitData);
		for (int i = 1; i < roomDataList.Count; i++) {
			RoomData roomData = roomDataList [i];
			if (roomData.ItemCount != 0) {
				mTargetChildTransform = childList [i];
				//最初は１番下から始める
				centerOnChild.CenterOn (childList [10]);
				return;
			}
		}
		//最初は１番下から始める
		centerOnChild.CenterOn (childList [10]);
	}

	private void Hoge (GameObject a) {
		if(mCenterd){
			return;
		}
		mCenterd = true;
		StartCoroutine (reset());
	}

	private IEnumerator reset(){
		yield return new WaitForSeconds (0.1f);
		if(mTargetChildTransform != null){
			centerOnChild.springStrength = 8.0f;
			centerOnChild.CenterOn (mTargetChildTransform);
		}

	}
}
