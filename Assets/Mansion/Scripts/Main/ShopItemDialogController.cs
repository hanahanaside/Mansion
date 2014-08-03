using UnityEngine;
using System.Collections;

public class ShopItemDialogController : DialogController {

	public void Init (ShopItemData shopItemData) {
		
	}
	
	public override void OnBuyButtonClicked () {
		base.OnBuyButtonClicked();
		Destroy (transform.parent.gameObject);
	}
	
	public override void OnCloseButonClicked () {
		base.OnCloseButonClicked();
		Destroy (transform.parent.gameObject);
	}

}
