using UnityEngine;
using System.Collections;

public class RoomController : MonoBehaviour {

	public GameObject buyButton;
	public GameObject roomItemDialogPrefab;

	public void Init(RoomData roomData){

	}

	public void OnBuyButtonClicked () {
		ShowBuyRoomItemDialog ();
		iTweenEvent removeEvent = iTweenEvent.GetEvent (buyButton, "ExitEvent");
		removeEvent.Play ();
	}

	private void ShowBuyRoomItemDialog () {
		GameObject uiRoot = GameObject.Find("UI Root");
		GameObject buyRoomItemDialogObject = Instantiate (roomItemDialogPrefab) as GameObject;
		buyRoomItemDialogObject.transform.parent = uiRoot.transform;
		buyRoomItemDialogObject.transform.localScale = new Vector3 (1, 1, 1);
		DialogController.itemBoughtEvent += itemBoughtEvent;
		DialogController.dialogClosedEvent += dialogClosedEvent;
	}

	public void itemBoughtEvent () {
		Debug.Log ("boughtEvent");
		Reset ();
	}

	public void dialogClosedEvent () {
		Debug.Log ("closedEvent");
		Reset ();
	}

	private void Reset () {
		DialogController.itemBoughtEvent -= itemBoughtEvent;
		DialogController.dialogClosedEvent -= dialogClosedEvent;
		iTweenEvent comeBackEvent = iTweenEvent.GetEvent (buyButton, "ComeBackEvent");
		comeBackEvent.Play ();
	}

}
