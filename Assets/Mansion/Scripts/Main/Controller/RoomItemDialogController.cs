using UnityEngine;
using System;
using System.Collections;

public class RoomItemDialogController : DialogController {
	public GameObject closeButton;
	public GameObject priceLabelObject;
	public UIAtlas roomItemAtlas;
	public UISprite itemSprite;
	public UILabel nameLabel;
	public UILabel countLabel;
	public UILabel descriptionLabel;
	private RoomData mRoomData;
	private TweenColor mShortMoneyTweenColor;
	private iTweenEvent mShortMoneyTweenScale;

	void Init (RoomData roomData) {
		Debug.Log ("roomId = " + roomData.Id);
		mRoomData = roomData;
		mShortMoneyTweenColor = priceLabelObject.GetComponent<TweenColor> ();
		mShortMoneyTweenScale = priceLabelObject.GetComponent<iTweenEvent>();
		string spriteName = "room_item_" + roomData.Id;
		UISpriteData spriteData = roomItemAtlas.GetSprite (spriteName);
		itemSprite.spriteName = "room_item_" + roomData.Id;
		itemSprite.width = (int)(spriteData.width * 1.5); 
		itemSprite.height = (int)(spriteData.height * 1.5);
		nameLabel.text = roomData.ItemName;
		UpdateIteminfoLabel ();
		descriptionLabel.text = roomData.ItemDescription;
	}

	void OnButtonEntranceEventFinished () {
		iTweenEvent.GetEvent (closeButton, "RotateEvent").Play ();
	}

	public override void OnBuyButtonClicked () {
		decimal keepMoneyCount = CountManager.Instance.KeepMoneyCount;
		if (keepMoneyCount < PriceCalculator.CalcRoomItemPrice (mRoomData)) {
			//short money
			SoundManager.Instance.PlaySE (AudioClipID.SE_SHORT_MONEY);
			mShortMoneyTweenColor.PlayForward ();
			mShortMoneyTweenScale.Play ();
			StartCoroutine (StopShortTween ());
		} else if (mRoomData.ItemCount == 0) {
			FirstItemBought ();
			Destroy (transform.parent.gameObject);
		} else {
			OnBuyApart ();
			UpdateIteminfoLabel ();
		}
	}

	public override void OnCloseButonClicked () {
		base.OnCloseButonClicked ();
		Destroy (transform.parent.gameObject);
	}

	private IEnumerator StopShortTween () {
		yield return new WaitForSeconds (1.0f);
		mShortMoneyTweenColor.enabled = false;
		mShortMoneyTweenScale.Stop ();
		priceLabelObject.GetComponent<UILabel> ().color = Color.black;
	}

	private void UpdateIteminfoLabel () {
		long totalPrice = PriceCalculator.CalcRoomItemPrice (mRoomData);
		priceLabelObject.GetComponent<UILabel> ().text = CommaMarker.MarkLongCount (totalPrice) + "円";
		countLabel.text = "\u6240\u6301\u6570 : " + mRoomData.ItemCount;
	}
}
