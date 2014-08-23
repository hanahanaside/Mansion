using UnityEngine;
using System.Collections;

public class ShopButtonController : MonoBehaviour {

	public GameObject shopItemDialogPrefab;
	public GameObject closedSprite;
	public GameObject unLockSprite;
	public GameObject lockSprite;
	private ShopItemData mShopItemData;

	void Init (ShopItemData shopItemData) {
		mShopItemData = shopItemData;
		if (mShopItemData.Level == ShopItemData.LEVEL_CLOSED) {
			unLockSprite.SetActive (false);
			lockSprite.SetActive (false);
		} else if (mShopItemData.Level == ShopItemData.LEVEL_LOCK) {
			closedSprite.SetActive (false);
		} else if (mShopItemData.Level == ShopItemData.LEVEL_UNLOCK) {
			closedSprite.SetActive (false);
			lockSprite.SetActive (false);
		}else if(mShopItemData.Level == ShopItemData.LEVEL_BOUGHT){

		}
	}
	
	public void OnButtonClicked () {
		GameObject uiRoot = UIRootInstanceKeeper.UIRootGameObject;
		GameObject shopItemDialog = Instantiate (shopItemDialogPrefab) as GameObject;
		shopItemDialog.transform.parent = uiRoot.transform;
		shopItemDialog.transform.localScale = new Vector3 (1, 1, 1);
		shopItemDialog.BroadcastMessage ("Init", mShopItemData);
		DialogController.itemBoughtEvent += itemBoughtEvent;
		DialogController.dialogClosedEvent += dialogClosedEvent;

	}

	public void itemBoughtEvent () {
		Debug.Log ("boughtEvent");
		Reset ();
	}
	
	public void dialogClosedEvent () {
		Debug.Log ("closedEvent");
		Reset ();
	}

	private void Reset () {
		DialogController.itemBoughtEvent -= itemBoughtEvent;
		DialogController.dialogClosedEvent -= dialogClosedEvent;
	}
}
