using UnityEngine;
using System;
using System.Collections;

public class RoomItemDialogController : DialogController {
	
	public void Init (RoomData roomData) {

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
