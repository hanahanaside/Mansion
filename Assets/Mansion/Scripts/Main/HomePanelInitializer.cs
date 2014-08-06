using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HomePanelInitializer : MonoBehaviour {

	public UIGrid grid;

	void OnEnable () {
		List<Transform> childList = grid.GetChildList ();
		List<RoomData> roomDataList = new List<RoomData> ();
		for (int i = 0; i<11; i++) {
			RoomData roomData = new RoomData ();
			roomData.Id = i;
			roomData.ItemCount = 1;
			roomData.ItemPrice = i * 10;
			roomData.GenerateSpeed = i + 1;
			roomData.ItemName = "itemName " + i;
			roomData.ItemDescription = "itemDescription " + i;
			roomDataList.Add (roomData);
		}
		for (int i = 0; i<11; i++) {
			GameObject childObject = childList [i].gameObject;
			RoomData roomData = roomDataList [i];
			childObject.BroadcastMessage ("Init", roomData);
		}
	}
}
