using UnityEngine;
using System.Collections;

public class ShopItemDialogController : DialogController {

	public UISprite itemSprite;
	public GameObject lockObject;
	public UILabel nameLabel;
	public UILabel priceLabel;
	public UILabel descriptionLabel;

	void Init (ShopItemData shopItemData) {
		nameLabel.text = shopItemData.Name;
		priceLabel.text = shopItemData.Price + "\u5186";
		itemSprite.spriteName = "shop_item_"+ shopItemData.Id;
		InitComponentsByLockLevel(shopItemData);
	}
	
	public override void OnBuyButtonClicked () {
		base.OnBuyButtonClicked ();
		Destroy (transform.parent.gameObject);
	}
	
	public override void OnCloseButonClicked () {
		base.OnCloseButonClicked ();
		Destroy (transform.parent.gameObject);
	}

	private void InitComponentsByLockLevel(ShopItemData shopItemData){
		if (shopItemData.UnlockLevel == ShopItemData.UNLOCK_LEVEL_LOCKED) {
			descriptionLabel.text = shopItemData.Description;
			lockObject.SetActive(true);
		} else if (shopItemData.UnlockLevel == ShopItemData.UNLOCK_LEVEL_UNLOCKED) {
			descriptionLabel.text = "ddddddddddddddd";
		}
	}
}
