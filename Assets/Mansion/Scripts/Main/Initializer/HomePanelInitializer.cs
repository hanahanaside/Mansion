using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HomePanelInitializer : MonoBehaviour {

	public UIGrid grid;

	void OnEnable () {
		List<Transform> childList = grid.GetChildList ();
		List<RoomData> roomDataList = RoomDataDao.Instance.GetRoomDataList();
		roomDataList.Reverse();
		for (int i = 10; i>=0; i--) {
			GameObject childObject = childList [i].gameObject;
			RoomData roomData = roomDataList [i];
			childObject.BroadcastMessage ("Init", roomData);
		}
	}
}
