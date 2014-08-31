using UnityEngine;
using System.Collections;

public class ShopButtonController : MonoBehaviour {

	public GameObject shopItemDialogPrefab;
	public GameObject parentObject;
	public UISprite questionSprite;
	public UISprite itemSprite;
	public UISprite lockSprite;
	public UISprite stampSprite;
	public iTweenEvent stampEvent;
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
		RemoveEvents ();
		mShopItemData.UnlockLevel = ShopItemData.UNLOCK_LEVEL_BOUGHT;
		ShopItemDataDao.Instance.UpdateUnLockLevel (mShopItemData.Id,mShopItemData.UnlockLevel);
		stampSprite.enabled = true;
		stampEvent.Play ();
		string tag = mShopItemData.Tag;
		if(tag == ShopItemData.TAG_ITEM){
			CountManager.Instance.UpdateGenerateSpeed ();
		}
		if(tag == ShopItemData.TAG_PIT){
			if(mShopItemData.Id < 5){
				ShopItemDataDao.Instance.UpdateUnLockLevel (mShopItemData.Id + 1, ShopItemData.UNLOCK_LEVEL_LOCKED);
			}
		}
	}
		
	public void dialogClosedEvent () {
		Debug.Log ("closedEvent");
		RemoveEvents ();
	}

	private void RemoveEvents () {
		DialogController.itemBoughtEvent -= itemBoughtEvent;
		DialogController.dialogClosedEvent -= dialogClosedEvent;
	}

	private void InitComponentsByLockLevel(){
		questionSprite.enabled = false;
		lockSprite.enabled = false;
		parentObject.collider.enabled = false;
		itemSprite.enabled = false;
		stampSprite.enabled = false;
		if(mShopItemData.UnlockLevel == ShopItemData.UNLOCK_LEVEL_CLOSED){
			questionSprite.enabled = true;
		}
		if(mShopItemData.UnlockLevel == ShopItemData.UNLOCK_LEVEL_LOCKED){
			lockSprite.enabled = true;
			parentObject.collider.enabled = true;
			ShowItemSprite();
		}
		if(mShopItemData.UnlockLevel == ShopItemData.UNLOCK_LEVEL_UNLOCKED){
			ShowItemSprite();
			parentObject.collider.enabled = true;
		}
		if(mShopItemData.UnlockLevel == ShopItemData.UNLOCK_LEVEL_BOUGHT){
			ShowItemSprite();
			stampSprite.enabled = true;
			parentObject.collider.enabled = true;
		}
		if(mShopItemData.UnlockLevel == ShopItemData.UNLOCK_LEVEL_STATUS){
			ShowItemSprite();
			parentObject.collider.enabled = true;
		}
	}

	private void ShowItemSprite(){
		itemSprite.enabled = true;
		itemSprite.spriteName = "shop_item_" + mShopItemData.Id;
	}
}
