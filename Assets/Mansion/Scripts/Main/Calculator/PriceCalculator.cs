using UnityEngine;
using System.Collections;

public class PriceCalculator {
	public static long CalcRoomItemPrice (RoomData roomData) {
		int itemCount = roomData.ItemCount;
		if (itemCount == 0) {
			return roomData.ItemPrice;
		}
		long totalPrice = roomData.ItemPrice;
		for (int i = 1; i <= roomData.ItemCount; i++) {
			long addPrice = totalPrice / 10;
			totalPrice += addPrice;
		}
		return totalPrice;
	}
}
