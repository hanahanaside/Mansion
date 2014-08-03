using UnityEngine;
using System.Collections;

public class RoomController : MonoBehaviour {

	public GameObject buyButton;
	public GameObject uiRoot;
	public GameObject buyRoomItemDialogPrefab;
	
	public void OnBuyButtonClicked () {
		ShowBuyRoomItemDialog ();
		iTweenEvent removeEvent = iTweenEvent.GetEvent (buyButton, "ExitEvent");
		removeEvent.Play ();
	}

	private void ShowBuyRoomItemDialog () {
		GameObject buyRoomItemDialogObject = Instantiate (buyRoomItemDialogPrefab) as GameObject;
		buyRoomItemDialogObject.transform.parent = uiRoot.transform;
		buyRoomItemDialogObject.transform.localScale = new Vector3 (1, 1, 1);
		BuyRoomItemDialogController.itemBoughtEvent += itemBoughtEvent;
		BuyRoomItemDialogController.dialogClosedEvent += dialogClosedEvent;
	}

	public void itemBoughtEvent () {
		Debug.Log ("boughtEvent");
		BuyRoomItemDialogController.itemBoughtEvent -= itemBoughtEvent;
		PlayComeBackEvent ();
	}

	public void dialogClosedEvent () {
		Debug.Log ("closedEvent");
		BuyRoomItemDialogController.dialogClosedEvent -= dialogClosedEvent;
		PlayComeBackEvent ();
	}

	private void PlayComeBackEvent () {
		iTweenEvent comeBackEvent = iTweenEvent.GetEvent (buyButton, "ComeBackEvent");
		comeBackEvent.Play ();
	}
}
