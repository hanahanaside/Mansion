using UnityEngine;
using System.Collections;

public class ShopItemData {

	public static int UNLOCK_LEVEL_CLOSED = 0;
	public static int UNLOCK_LEVEL_LOCKED = 1;
	public static int UNLOCK_LEVEL_UNLOCKED = 2;
	public static int UNLOCK_LEVEL_BOUGHT = 3;

	public int Id{ get; set; }

	public int Price{ get; set; }

	public int UnlockLevel{ get; set; }

	public int UnLockCondition{ get; set; }

	public int TargetRoomId{ get; set; }

	public int Effect{ get; set; }

	public string Name{ get; set; }

	public string Description{ get; set; }

	public string Tag{ get; set; }
	
}
