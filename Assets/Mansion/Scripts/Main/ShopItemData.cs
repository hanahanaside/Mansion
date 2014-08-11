using UnityEngine;
using System.Collections;

public class ShopItemData {

	public static int LEVEL_CLOSED = 0;
	public static int LEVEL_LOCK = 1;
	public static int LEVEL_UNLOCK = 2;
	public static int LEVEL_BOUGHT = 3;

	public int Id{ get; set; }

	public int Price{ get; set; }

	public int Level{ get; set; }

	public string Name{ get; set; }

	public string LockDescription{ get; set; }

	public string UnLockDescription{ get; set; }
}
