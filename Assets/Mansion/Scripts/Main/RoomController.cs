using UnityEngine;
using System.Collections;

public class RoomController : MonoBehaviour {

	public GameObject buyButton;
	public GameObject roomItemDialogPrefab;
	public UILabel nameLabel;
	public UILabel generateSpeedLabel;
	private RoomData mRoomData;

	void Init (RoomData roomData) {
		mRoomData = roomData;
		SetTextData ();
	}

	public void OnBuyButtonClicked () {
		ShowBuyRoomItemDialog ();
		iTweenEvent removeEvent = iTweenEvent.GetEvent (buyButton, "ExitEvent");
		removeEvent.Play ();
	}

	private void ShowBuyRoomItemDialog () {
		GameObject roomItemDialog = Instantiate (roomItemDialogPrefab) as GameObject;
		roomItemDialog.transform.parent = RootInstanceKeeper.Instance.transform;
		roomItemDialog.transform.localScale = new Vector3 (1, 1, 1);
		roomItemDialog.BroadcastMessage ("Init", mRoomData);
		DialogController.itemBoughtEvent += itemBoughtEvent;
		DialogController.dialogClosedEvent += dialogClosedEvent;
	}

	public void itemBoughtEvent () {
		Debug.Log ("boughtEvent");
		// update database
		mRoomData.ItemCount++;
		CountManager.Instance.AddGenerateSpeed (mRoomData.GenerateSpeed);
		SetTextData ();
		Reset ();
	}

	public void dialogClosedEvent () {
		Debug.Log ("closedEvent");
		Reset ();
	}

	private void SetTextData () {
		nameLabel.text = mRoomData.ItemName + " : " + mRoomData.ItemCount;
		generateSpeedLabel.text = (mRoomData.GenerateSpeed * mRoomData.ItemCount) + " / \u79d2";
	}

	private void Reset () {
		DialogController.itemBoughtEvent -= itemBoughtEvent;
		DialogController.dialogClosedEvent -= dialogClosedEvent;
		iTweenEvent comeBackEvent = iTweenEvent.GetEvent (buyButton, "ComeBackEvent");
		comeBackEvent.Play ();
	}

}
