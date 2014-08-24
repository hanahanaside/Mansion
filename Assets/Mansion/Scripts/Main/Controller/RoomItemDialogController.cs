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
		mShortMoneyTweenColor = priceLabelObject.GetComponent<TweenColor>();
		string spriteName = "room_item_" + roomData.Id;
		UISpriteData spriteData = roomItemAtlas.GetSprite (spriteName);
		itemSprite.spriteName = "room_item_" + roomData.Id;
		itemSprite.width = spriteData.width;
		itemSprite.height = spriteData.height;
		nameLabel.text = roomData.ItemName;
		priceLabelObject.GetComponent<UILabel>().text  = "price : " + roomData.ItemPrice;
		countLabel.text = "所持数 : " + roomData.ItemCount;
		descriptionLabel.text = roomData.ItemDescription;
	}

	void OnButtonEntranceEventFinished () {
		iTweenEvent.GetEvent(closeButton,"RotateEvent").Play();
	}

	public override void OnBuyButtonClicked () {
		int keepMoneyCount = CountManager.Instance.KeepMoneyCount;
		if(keepMoneyCount < mRoomData.ItemPrice){
			//short money
			SoundManager.Instance.PlaySE(AudioClipID.SE_SHORT_MONEY);
			mShortMoneyTweenColor.PlayForward();
			StartCoroutine(StopShortTween());
		}else {
			base.OnBuyButtonClicked ();
		}
	}

	public override void OnCloseButonClicked () {
		base.OnCloseButonClicked ();
		Destroy (transform.parent.gameObject);
	}

	private IEnumerator StopShortTween(){
		yield return new WaitForSeconds(1.0f);
		mShortMoneyTweenColor.enabled = false;
		priceLabelObject.GetComponent<UILabel>().color = Color.black;
	}
}
