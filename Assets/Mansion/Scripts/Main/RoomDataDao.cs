using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomDataDao {

	private static RoomDataDao sInstance;

	public static RoomDataDao Instance {
		get {
			if (sInstance == null) {
				sInstance = new RoomDataDao ();
			}
			return sInstance;
		}
	}

	public List<RoomData> GetRoomDataList () {
		List<RoomData> roomDataList = new List<RoomData> ();
		for (int i = 1; i<12; i++) {
			RoomData roomData = new RoomData ();
			roomData.Id = i;
			roomData.ItemCount = 1;
			roomData.ItemPrice = i * 10;
			roomData.GenerateSpeed = i + 1;
			roomData.ItemName = "itemName " + i;
			roomData.ItemDescription = "itemDescription " + i;
			roomDataList.Add (roomData);
		}
		return roomDataList;
	}

	public float GetTotalGenerateSpeed () {
		List<RoomData> roomDataList = new List<RoomData> ();
		float totalGenerateSpeed = 0;
		for (int i = 0; i<10; i++) {
			RoomData roomData = new RoomData ();
			roomData.ItemCount = 1;
			roomData.GenerateSpeed = 1;
			roomDataList.Add (roomData);
		}
		foreach (RoomData roomData in roomDataList) {
			totalGenerateSpeed += roomData.GenerateSpeed * roomData.ItemCount;
		}
		return totalGenerateSpeed;
	}
}
