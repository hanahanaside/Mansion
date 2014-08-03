using UnityEngine;
using System;
using System.Collections;

public class BuyRoomItemDialogController : MonoBehaviour {

	public static event Action itemBoughtEvent;
	public static event Action dialogClosedEvent;

	public void Init (RoomData roomData) {

	}

	public void OnBuyButtonClicked () {
		itemBoughtEvent ();
		Destroy (transform.parent.gameObject);
	}

	public void OnCloseButonClicked () {
		dialogClosedEvent ();
		Destroy (transform.parent.gameObject);
	}
}
