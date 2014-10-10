using UnityEngine;
using System.Collections;
using System.Text;

public class ShopItemDialogController : DialogController {
	public UISprite itemSprite;
	public GameObject lockObject;
	public GameObject priceLabelObject;
	public GameObject buyButtonObject;
	public UILabel nameLabel;
	public UILabel descriptionLabel;
	private ShopItemData mShopItemData;
	private TweenColor mShortMoneyTweenColor;
	private iTweenEvent mShortMoneyScaleEvent;
	private bool mAnimationPlaying = false;

	void Init (ShopItemData shopItemData) {
		mShopItemData = shopItemData;
		mShortMoneyTweenColor = priceLabelObject.GetComponent<TweenColor> ();
		mShortMoneyScaleEvent = priceLabelObject.GetComponent<iTweenEvent>();
		nameLabel.text = shopItemData.Name;
		SetPriceLabel (shopItemData.Price);
		InitComponentsByTag (shopItemData);
	}

	public override void OnBuyButtonClicked () {
		decimal keepMoneyCount = CountManager.Instance.KeepMoneyCount;
		if (keepMoneyCount < mShopItemData.Price) {
			SoundManager.Instance.PlaySE (AudioClipID.SE_SHORT_MONEY);
			if (!mAnimationPlaying) {
				mAnimationPlaying = true;
				mShortMoneyTweenColor.PlayForward ();
				mShortMoneyScaleEvent.Play ();
				StartCoroutine (StopShortTween ());
			}
			return;
		} 

		if (mShopItemData.Tag == ShopItemData.TAG_SECOM) {
			BuySecom ();
		} else {
			CountManager.Instance.DecreaseMoneyCount (mShopItemData.Price);
			OnBuyShopItem ();
			FenceManager.Instance.HideFence ();
			Destroy (transform.parent.gameObject);
		}
	}

	private void BuySecom () {
		SecomData secomData = PrefsManager.Instance.GetSecomData ();
		if (secomData.Count >= 10) {
			SoundManager.Instance.PlaySE (AudioClipID.SE_SHORT_MONEY);
			Debug.Log ("retur ");
			return;
		}
		CountManager.Instance.DecreaseMoneyCount (mShopItemData.Price);
		secomData.Count++;
		PrefsManager.Instance.SaveSecomData (secomData);
		mShopItemData.Price = secomData.Price;
		SetPriceLabel (secomData.Price);
		base.OnBuyButtonClicked ();
	}

	public override void OnCloseButonClicked () {
		base.OnCloseButonClicked ();
		Destroy (transform.parent.gameObject);
	}

	private void InitComponentsByTag (ShopItemData shopItemData) {
		string tag = shopItemData.Tag;
		if (tag == ShopItemData.TAG_PIT) {
			itemSprite.spriteName = "shop_item_" + shopItemData.Id;
			InitPitComponents (shopItemData);
		}

		if (tag == ShopItemData.TAG_ITEM) {
			itemSprite.spriteName = "shop_item_" + shopItemData.Id;
			InitItemComponents (shopItemData);
		}

		if (tag == ShopItemData.TAG_SECOM) {
			itemSprite.spriteName = "bt_secom";
			InitSecomComponents (shopItemData);
		}
	}

	private void InitSecomComponents (ShopItemData shopItemData) {
		nameLabel.text = "\u30a2\u30eb\u30c4\u30c3\u30af";
		descriptionLabel.text = shopItemData.Description;
	}

	private void InitPitComponents (ShopItemData shopItemData) {
		StringBuilder sb = new StringBuilder ();
		if (shopItemData.UnlockLevel == ShopItemData.UNLOCK_LEVEL_LOCKED) {
			lockObject.SetActive (true);
			sb.Append ("\u9271\u5c71\u3092");
			sb.Append (shopItemData.UnLockCondition + "\u56de\u30bf\u30c3\u30d7\u3059\u308b\u3068\u30a2\u30f3\u30ed\u30c3\u30af");
		}
		if (shopItemData.UnlockLevel == ShopItemData.UNLOCK_LEVEL_UNLOCKED) {
				sb.Append (shopItemData.Description + "\n");
			sb.Append ("1\u56de\u306e\u30bf\u30c3\u30d7\u3067");
			sb.Append (shopItemData.Effect + "\u5186\u7372\u5f97");
		}
		descriptionLabel.text = sb.ToString ();
	}

	private void InitItemComponents (ShopItemData shopItemData) {
		string description = "";
		RoomData roomData = RoomDataDao.Instance.GetRoomDataById (mShopItemData.TargetRoomId);
		if (shopItemData.UnlockLevel == ShopItemData.UNLOCK_LEVEL_LOCKED) {
			lockObject.SetActive (true);
			description = roomData.ItemName + shopItemData.UnLockCondition + "\u500b\u3067\u30a2\u30f3\u30ed\u30c3\u30af";
		}
		if (shopItemData.UnlockLevel == ShopItemData.UNLOCK_LEVEL_UNLOCKED) {
			StringBuilder sb = new StringBuilder ();
			sb.Append (shopItemData.Description + "。");
			sb.Append (roomData.ItemName + "\u306e\u751f\u7523\u91cf" + shopItemData.Effect + "\u500d");
			description = sb.ToString ();
		}
		if (shopItemData.UnlockLevel == ShopItemData.UNLOCK_LEVEL_STATUS) {
			StringBuilder sb = new StringBuilder ();
			sb.Append (shopItemData.Description + "。");
			sb.Append (roomData.ItemName + "\u306e\u751f\u7523\u91cf" + shopItemData.Effect + "\u500d");
			description = sb.ToString ();
			buyButtonObject.SetActive (false);
		}
		descriptionLabel.text = description;
	}

	private IEnumerator StopShortTween () {
		yield return new WaitForSeconds (1.0f);
		mShortMoneyTweenColor.enabled = false;
		mShortMoneyScaleEvent.Stop ();
		priceLabelObject.GetComponent<UILabel> ().color = Color.black;
		priceLabelObject.transform.localScale = new Vector3 (1,1,1);
		mAnimationPlaying = false;
	}

	private void SetPriceLabel(long price){
		UILabel priceLael = priceLabelObject.GetComponent<UILabel> ();
		priceLael.text = CommaMarker.MarkLongCount (price) + "\u5186";
	}
}
