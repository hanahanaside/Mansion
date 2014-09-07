using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomController : MonoBehaviour {
	public GameObject lockObject;
	public GameObject buyButtonParent;
	public GameObject buyButton;
	public GameObject roomItemDialogPrefab;
	public GameObject residentPrefab;
	public UILabel nameLabel;
	public UILabel generateSpeedLabel;
	public UIGrid[] itemGridArray;
	private RoomData mRoomData;
	private List<UISprite> mItemSpriteList;
	private List<ShopItemData> mShopItemDataList;

	void Init (RoomData roomData) {  
		mRoomData = roomData;
		if (mShopItemDataList == null) {
			mShopItemDataList = ShopItemDataDao.Instance.QueryByTargetRoomId (mRoomData.Id);
		}
		if (mItemSpriteList == null) {
			CreateItemSpriteList ();
		}
		if (mRoomData.ItemCount == 0) {
			lockObject.SetActive (true);
			return;
		}
		// only first
		UISprite firstItemSprite = mItemSpriteList [0];
		if (!firstItemSprite.enabled) {
			SetActiveItem ();
			GenerateResident (mRoomData.ItemCount);
		}
		SetTextData (); 
	}

	void EnemyGenerated(){
		nameLabel.color = Color.red;
		generateSpeedLabel.color = Color.red;
	}

	void EnemyDestroyed(){
		nameLabel.color = Color.white;
		generateSpeedLabel.color = Color.white;
	}

	void itemBoughtEvent () {
		Debug.Log ("boughtEvent");

		if (mRoomData.ItemCount == 0) {
			lockObject.SetActive (false);

			//ルームIDに対応するアイテムを全てロック状態にする
			foreach (ShopItemData shopItemData in mShopItemDataList) {
				ShopItemDataDao.Instance.UpdateUnLockLevel (shopItemData.Id, ShopItemData.UNLOCK_LEVEL_LOCKED);
			}
		}
		CountManager.Instance.DecreaseMoneyCount (PriceCalculator.CalcRoomItemPrice (mRoomData));
		mRoomData.ItemCount++;

		RoomDataDao.Instance.UpdateItemCount (mRoomData);

		//新規にアンロックできるアイテムがあればアンロック
		foreach (ShopItemData shopItemData in mShopItemDataList) {
			if (shopItemData.UnlockLevel == ShopItemData.UNLOCK_LEVEL_BOUGHT) {
				continue;
			}
			int unlockCondition = shopItemData.UnLockCondition;
			if (mRoomData.ItemCount >= unlockCondition) {
				ShopItemDataDao.Instance.UpdateUnLockLevel (shopItemData.Id, ShopItemData.UNLOCK_LEVEL_UNLOCKED);
			}
		}

		SetActiveItem ();
		CountManager.Instance.AddGenerateSpeed (mRoomData.GenerateSpeed);
		SetTextData ();
		GenerateResident (1);
	}

	void dialogClosedEvent () {
		Debug.Log ("closedEvent");
		Reset ();
	}

	void ComeBackEventFinished () {
		Debug.Log ("come back");
		iTweenEvent resetRotateEvent = iTweenEvent.GetEvent (buyButton, "ResetRotateEvent");
		resetRotateEvent.Play ();
	}

	public void OnBuyButtonClicked () {
		SoundManager.Instance.PlaySE (AudioClipID.SE_BUTTON);
		ShowRoomItemDialog ();
		iTweenEvent removeEvent = iTweenEvent.GetEvent (buyButtonParent, "ExitEvent");
		removeEvent.Play ();
	}

	private void ShowRoomItemDialog () { 
		GameObject roomItemDialog = Instantiate (roomItemDialogPrefab) as GameObject;
		roomItemDialog.transform.parent = UIRootInstanceKeeper.UIRootGameObject.transform;
		roomItemDialog.transform.localScale = new Vector3 (1, 1, 1);
		roomItemDialog.BroadcastMessage ("Init", mRoomData);
		DialogController.itemBoughtEvent += itemBoughtEvent;
		DialogController.dialogClosedEvent += dialogClosedEvent;
	}

	private void SetTextData () {
		nameLabel.text = mRoomData.ItemName + " : " + mRoomData.ItemCount;
		generateSpeedLabel.text = (mRoomData.GenerateSpeed * mRoomData.ItemCount) + " / \u79d2";
	}

	private void Reset () {
		DialogController.itemBoughtEvent -= itemBoughtEvent;
		DialogController.dialogClosedEvent -= dialogClosedEvent;
		buyButton.transform.eulerAngles = new Vector3 (0, 0, 45);
		iTweenEvent comeBackEvent = iTweenEvent.GetEvent (buyButtonParent, "ComeBackEvent");
		comeBackEvent.Play ();
	}

	private void SetActiveItem () {
		if (mRoomData.ItemCount > mItemSpriteList.Count) {
			return;
		}
		for (int i = 0; i < mRoomData.ItemCount; i++) {
			UISprite sprite = mItemSpriteList [i];
			if (!sprite.enabled) {
				sprite.enabled = true;
			}
		}
	}

	private void CreateItemSpriteList () {
		mItemSpriteList = new List<UISprite> ();
		foreach (UIGrid grid in itemGridArray) {
			List<Transform> itemList = grid.GetChildList ();
			foreach (Transform item in itemList) {
				UISprite sprite = item.GetComponent<UISprite> ();
				mItemSpriteList.Add (sprite);
			}
		}
	}

	private void GenerateResident (int count) {
		for (int i = 0; i < count; i++) {
			float x = Random.Range (-200, 200);
			float y = Random.Range (-100, 50);
			GameObject resident = Instantiate (residentPrefab) as GameObject;
			resident.transform.parent = transform.parent;
			resident.transform.localScale = new Vector3 (1, 1, 1);
			resident.transform.localPosition = new Vector3 (x, y, 0);
		}
	}
}
