﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomController : MonoBehaviour {
	public GameObject[] hideObjectsArray;
	public GameObject lockObject;
	public GameObject buyButtonParent;
	public GameObject buyButton;
	public GameObject roomItemDialogPrefab;
	public GameObject residentPrefab;
	public UILabel nameLabel;
	public UILabel generateSpeedLabel;
	public UISprite leftBackGround;
	public UISprite rightBackGround;
	public UISprite exSprite;
	public UnLockAnimator unlockAnimator;
	public UIGrid[] itemGridArray;
	private RoomData mRoomData;
	private List<UISprite> mItemSpriteList;
	private List<ShopItemData> mShopItemDataList;

	void Init (RoomData roomData) {  
		mRoomData = roomData;
		foreach (GameObject hideObject in hideObjectsArray) {
			hideObject.SetActive (true);
		}
		if (mShopItemDataList == null) {
			mShopItemDataList = ShopItemDataDao.Instance.QueryByTargetRoomId (mRoomData.Id);
		}
		if (mItemSpriteList == null) {
			CreateItemSpriteList ();
		}
		if (mRoomData.ItemCount == 0) {
			return;
		}
		lockObject.SetActive (false);

		// only first
		UISprite firstItemSprite = mItemSpriteList [0];
		if (!firstItemSprite.enabled) {
			SetActiveItem ();
			#if UNITY_IPHONE
			GenerateResident (mRoomData.ItemCount);
			#endif

			#if UNITY_ANDROID
			if(mRoomData.ItemCount > 30){
				GenerateResident (30);
			}else {
				GenerateResident (mRoomData.ItemCount);
			}
			#endif

		}
		SetTextData (); 
	}

	void Hide () {
		foreach (GameObject hideObject in hideObjectsArray) {
			hideObject.SetActive (false);
		}
	}

	void EnemyGenerated () {
		leftBackGround.spriteName = "bg_header_red";
		rightBackGround.spriteName = "bg_header_red";
	}

	void EnemyDestroyed () {
		leftBackGround.spriteName = "bg_header";
		rightBackGround.spriteName = "bg_header";
	}

	void itemBoughtEvent () {
		Debug.Log ("boughtEvent");

		if (mRoomData.ItemCount == 0) {
			unlockAnimator.PlayAnimation ();

			//ルームIDに対応するアイテムを全てロック状態にする
			foreach (ShopItemData shopItemData in mShopItemDataList) {
				ShopItemDataDao.Instance.UpdateUnLockLevel (shopItemData.Id, ShopItemData.UNLOCK_LEVEL_LOCKED);
				mShopItemDataList = ShopItemDataDao.Instance.QueryByTargetRoomId (mRoomData.Id);
			}
		}
		CountManager.Instance.DecreaseMoneyCount (PriceCalculator.CalcRoomItemPrice (mRoomData));
		mRoomData.ItemCount++;

		RoomDataDao.Instance.UpdateItemCount (mRoomData);

		//新規にアンロックできるアイテムがあればアンロック
		foreach (ShopItemData shopItemData in mShopItemDataList) {
			if (shopItemData.UnlockLevel != ShopItemData.UNLOCK_LEVEL_LOCKED) {
				continue;
			}
			int unlockCondition = shopItemData.UnLockCondition;
			if (mRoomData.ItemCount >= unlockCondition) {
				ShopItemDataDao.Instance.UpdateUnLockLevel (shopItemData.Id, ShopItemData.UNLOCK_LEVEL_UNLOCKED);
				mShopItemDataList = ShopItemDataDao.Instance.QueryByTargetRoomId (mRoomData.Id);
				exSprite.enabled = true;
			}
		}

		if (mRoomData.ItemCount <= mItemSpriteList.Count) {
			SetActiveItem ();
		}

		#if UNITY_IPHONE
		GenerateResident (1);
		#endif

		#if UNITY_ANDROID
		if(mRoomData.ItemCount < 30){
			GenerateResident (1);
		}

		#endif


		CountManager.Instance.AddGenerateSpeed (mRoomData.GenerateSpeed);
		SetTextData ();
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
		int effect = ShopItemDataDao.Instance.GetEffectByRoomId (mRoomData.Id);
		decimal generateSpeed = mRoomData.GenerateSpeed * mRoomData.ItemCount * effect;
		Debug.Log ("geneda = " + generateSpeed);
		//decimal generateSpeed = 0.5m;
		generateSpeedLabel.text = CommaMarker.MarkGenerateSpeed(generateSpeed) + " / \u79d2";
		//generateSpeedLabel.text = "" + generateSpeed;
		nameLabel.text = mRoomData.ItemName + " : " + mRoomData.ItemCount;
	}

	private void Reset () {
		DialogController.itemBoughtEvent -= itemBoughtEvent;
		DialogController.dialogClosedEvent -= dialogClosedEvent;
		buyButton.transform.eulerAngles = new Vector3 (0, 0, 45);
		iTweenEvent comeBackEvent = iTweenEvent.GetEvent (buyButtonParent, "ComeBackEvent");
		comeBackEvent.Play ();
	}

	private void SetActiveItem () {
		for (int i = 0; i < mRoomData.ItemCount; i++) {
			if (i >= mItemSpriteList.Count) {
				break;
			}
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
				sprite.enabled = false;
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
			resident.SendMessage ("SetDepth", mRoomData.Id);
		}
	}
}
