using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomController : MonoBehaviour {

	public GameObject buyButton;
	public GameObject roomItemDialogPrefab;
	public UILabel nameLabel;
	public UILabel generateSpeedLabel;
	public UIGrid frontGrid;
	public UIGrid backGrid;
	private RoomData mRoomData;
	private List<GameObject> mItemList;
	
	void Init (RoomData roomData) {
		mRoomData = roomData;
		SetTextData ();
		if (mItemList == null) {
			CreateItemList ();
			SetActiveItem ();
		}
	}

	public void OnBuyButtonClicked () {
		if(CheckInActiveItemExist()){
			ShowRoomItemDialog ();
			iTweenEvent removeEvent = iTweenEvent.GetEvent (buyButton, "ExitEvent");
			removeEvent.Play ();
		}else {
			//full of item
		}
	}

	private bool CheckInActiveItemExist () {
		foreach (GameObject item in mItemList) {
			if (!item.activeSelf) {
				return true;
			}
		}
		return false;
	}

	private void ShowRoomItemDialog () { 
		GameObject roomItemDialog = Instantiate (roomItemDialogPrefab) as GameObject;
		roomItemDialog.transform.parent = UIRootInstanceKeeper.UIRootGameObject.transform;
		roomItemDialog.transform.localScale = new Vector3 (1, 1, 1);
		roomItemDialog.BroadcastMessage ("Init", mRoomData);
		DialogController.itemBoughtEvent += itemBoughtEvent;
		DialogController.dialogClosedEvent += dialogClosedEvent;
	}

	public void itemBoughtEvent () {
		Debug.Log ("boughtEvent");
		// update database
		mRoomData.ItemCount++;
		SetActiveItem ();
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

	private void CreateItemList () {
		mItemList = new List<GameObject> ();
		List<Transform> frontItemList = frontGrid.GetChildList ();
		List<Transform> backItemList = backGrid.GetChildList ();
		foreach (Transform child in frontItemList) {
			mItemList.Add (child.gameObject);
			child.gameObject.SetActive (false);
		}
		foreach (Transform child in backItemList) {
			mItemList.Add (child.gameObject);
			child.gameObject.SetActive (false);
		}
	}

	private void SetActiveItem () {
		for (int i = 0; i < mRoomData.ItemCount; i++) {
			GameObject item = mItemList [i];
			item.SetActive (true);
		}
	}
}
