using UnityEngine;
using System.Collections;

public class ShopButtonController : MonoBehaviour {

	public GameObject shopItemDialogPrefab;
	public GameObject parentObject;
	public UISprite QuestionSprite;
	public UISprite itemSprite;
	public UISprite lockSprite;
	public UISprite stampSprite;
	private ShopItemData mShopItemData;
	
	void Init (ShopItemData shopItemData) {
		mShopItemData = shopItemData;
		InitComponentsByLockLevel();
	}
	
	public void OnButtonClicked () {
		SoundManager.Instance.PlaySE(AudioClipID.SE_BUTTON);
		if(mShopItemData.UnlockLevel == ShopItemData.UNLOCK_LEVEL_BOUGHT){
			return;
		}
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

	private void InitComponentsByLockLevel(){
		if(mShopItemData.UnlockLevel == ShopItemData.UNLOCK_LEVEL_CLOSED){
			QuestionSprite.enabled = true;
			parentObject.collider.enabled = false;
		}else if(mShopItemData.UnlockLevel == ShopItemData.UNLOCK_LEVEL_LOCKED){
			lockSprite.enabled = true;
			ShowItemSprite();
		}else if(mShopItemData.UnlockLevel == ShopItemData.UNLOCK_LEVEL_UNLOCKED){
			ShowItemSprite();
		}else if(mShopItemData.UnlockLevel == ShopItemData.UNLOCK_LEVEL_BOUGHT){
			ShowItemSprite();
			stampSprite.enabled = true;
		}

	}

	private void ShowItemSprite(){
		itemSprite.enabled = true;
		itemSprite.spriteName = "shop_item_" + mShopItemData.Id;
	}
}
