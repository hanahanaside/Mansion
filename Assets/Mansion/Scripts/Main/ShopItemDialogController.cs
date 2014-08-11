using UnityEngine;
using System.Collections;

public class ShopItemDialogController : DialogController {

	public UISprite itemSprite;
	public GameObject lockSprite;
	public UILabel nameLabel;
	public UILabel priceLabel;
	public UILabel descriptionLabel;

	void Init (ShopItemData shopItemData) {
		nameLabel.text = shopItemData.Name;
		if (shopItemData.Level == ShopItemData.LEVEL_LOCK) {
			descriptionLabel.text = shopItemData.LockDescription;
		} else if (shopItemData.Level == ShopItemData.LEVEL_UNLOCK) {
			descriptionLabel.text = shopItemData.UnLockDescription;
			lockSprite.SetActive (false);
		}
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
