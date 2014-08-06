using UnityEngine;
using System;
using System.Collections;

public class RoomItemDialogController : DialogController {

	public UISprite itemSprite;
	public UILabel nameLabel;
	public UILabel priceLabel;
	public UILabel countLabel;
	public UILabel descriptionLabel;

	void Init (RoomData roomData) {
		Debug.Log ("roomId = " + roomData.Id);
		nameLabel.text = roomData.ItemName;
		priceLabel.text = "price : " + roomData.ItemPrice;
		countLabel.text = "所持数 : " + roomData.ItemCount;
		descriptionLabel.text = roomData.ItemDescription;
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
