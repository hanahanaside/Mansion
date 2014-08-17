using UnityEngine;
using System;
using System.Collections;

public class RoomItemDialogController : DialogController {

	public GameObject closeButton;
	public UIAtlas roomItemAtlas;
	public UISprite itemSprite;
	public UILabel nameLabel;
	public UILabel priceLabel;
	public UILabel countLabel;
	public UILabel descriptionLabel;

	void Init (RoomData roomData) {
		Debug.Log ("roomId = " + roomData.Id);
		string spriteName = "room_item_" + roomData.Id;
		UISpriteData spriteData = roomItemAtlas.GetSprite (spriteName);
		itemSprite.spriteName = "room_item_" + roomData.Id;
		itemSprite.width = spriteData.width;
		itemSprite.height = spriteData.height;
		nameLabel.text = roomData.ItemName;
		priceLabel.text = "price : " + roomData.ItemPrice;
		countLabel.text = "所持数 : " + roomData.ItemCount;
		descriptionLabel.text = roomData.ItemDescription;
	}

	void OnButtonEntranceEventFinished () {
		iTweenEvent buttonRotateEvent = iTweenEvent.GetEvent (closeButton, "RotateEvent");
		buttonRotateEvent.Play ();
	}

	public override void OnBuyButtonClicked () {
		base.OnBuyButtonClicked ();
		Destroy (transform.parent.gameObject);
	}

	public override void OnCloseButonClicked () {
		base.OnCloseButonClicked ();
		Destroy (transform.parent.gameObject);
	}
}
