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

	void Init (RoomData roomData) {
		Debug.Log ("roomId = " + roomData.Id);
		mRoomData = roomData;
		mShortMoneyTweenColor = priceLabelObject.GetComponent<TweenColor> ();
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
			StartCoroutine (StopShortTween ());
		} else {
			base.OnBuyButtonClicked ();
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
		priceLabelObject.GetComponent<UILabel> ().color = Color.black;
	}

	private void UpdateIteminfoLabel () {
		long totalPrice = PriceCalculator.CalcRoomItemPrice (mRoomData);
		priceLabelObject.GetComponent<UILabel> ().text = CommaMarker.MarkLongCount (totalPrice) + "円";
		countLabel.text = "\u6240\u6301\u6570 : " + mRoomData.ItemCount;
	}
}
