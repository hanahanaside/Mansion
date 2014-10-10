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
	public iTweenEvent updateCountEvent;
	private RoomData mRoomData;
	private TweenColor mShortMoneyTweenColor;
	private iTweenEvent mShortMoneyTweenScale;
	private bool mAnimationPlaying = false;

	void Init (RoomData roomData) {
		Debug.Log ("roomId = " + roomData.Id);
		mRoomData = roomData;
		mShortMoneyTweenColor = priceLabelObject.GetComponent<TweenColor> ();
		mShortMoneyTweenScale = priceLabelObject.GetComponent<iTweenEvent> ();
		string spriteName = "";
		if (roomData.ItemCount == 0) {
			spriteName = "room_item_" + roomData.Id + "_lock";
			nameLabel.text = "????";
			descriptionLabel.text = "??????????????????";
		} else {
			spriteName = "room_item_" + roomData.Id;
			nameLabel.text = roomData.ItemName;
			descriptionLabel.text = roomData.ItemDescription;
		}
		UISpriteData spriteData = roomItemAtlas.GetSprite (spriteName);
		itemSprite.spriteName = spriteName;
		itemSprite.width = (int)(spriteData.width * 1.5); 
		itemSprite.height = (int)(spriteData.height * 1.5);

		UpdateIteminfoLabel ();

	}

	void OnButtonEntranceEventFinished () {
		iTweenEvent.GetEvent (closeButton, "RotateEvent").Play ();
	}

	public override void OnBuyButtonClicked () {
		decimal keepMoneyCount = CountManager.Instance.KeepMoneyCount;
		if (keepMoneyCount < PriceCalculator.CalcRoomItemPrice (mRoomData)) {
			//short money
			SoundManager.Instance.PlaySE (AudioClipID.SE_SHORT_MONEY);
			if (!mAnimationPlaying) {
				mAnimationPlaying = true;
				mShortMoneyTweenColor.PlayForward ();
				mShortMoneyTweenScale.Play ();
				StartCoroutine (StopShortTween ());
			}
		} else if (mRoomData.ItemCount == 0) {
			FirstItemBought ();
			Destroy (transform.parent.gameObject);
		} else {
			OnBuyApart ();
			UpdateIteminfoLabel ();
			updateCountEvent.Play ();
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
		priceLabelObject.transform.localScale = new Vector3 (1, 1, 1);
		mAnimationPlaying = false;
	}

	private void UpdateIteminfoLabel () {
		long totalPrice = PriceCalculator.CalcRoomItemPrice (mRoomData);
		priceLabelObject.GetComponent<UILabel> ().text = CommaMarker.MarkLongCount (totalPrice) + "円";
		countLabel.text = "\u6240\u6301\u6570 : " + mRoomData.ItemCount;
	}
}
