using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomController : MonoBehaviour {

	public GameObject lockObject;
	public GameObject buyButtonParent;
	public GameObject buyButton;
	public GameObject roomItemDialogPrefab;
	public GameObject residentPrefab;
	public UILabel nameLabel;
	public UILabel generateSpeedLabel;
	public UIGrid frontGrid;
	public UIGrid backGrid;
	private RoomData mRoomData;
	private List<GameObject> mItemList;
	
	void Init (RoomData roomData) {
		mRoomData = roomData;
		if (mRoomData.ItemCount == 0) {
			lockObject.SetActive(true);
			return;
		}
		SetTextData ();
		if (mItemList == null) {
			CreateItemList ();
			SetActiveItem ();
			GenerateResident (mRoomData.ItemCount);
		}
	}

	void itemBoughtEvent () {
		Debug.Log ("boughtEvent");
		// update database
		mRoomData.ItemCount++;
		SetActiveItem ();
		CountManager.Instance.AddGenerateSpeed (mRoomData.GenerateSpeed);
		SetTextData ();
		GenerateResident (1);
		Reset ();
	}
	
	void dialogClosedEvent () {
		Debug.Log ("closedEvent");
		Reset ();
	}

	void ComeBackEventFinished () {
		Debug.Log ("come back");
		iTweenEvent resetRotateEvent = iTweenEvent.GetEvent (buyButton, "ResetRotateEvent");
		resetRotateEvent.Play ();
	}

	public void OnBuyButtonClicked () {
		if (CheckInActiveItemExist ()) {
			ShowRoomItemDialog ();
			iTweenEvent removeEvent = iTweenEvent.GetEvent (buyButtonParent, "ExitEvent");
			removeEvent.Play ();
		} else {
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

	private void SetTextData () {
		nameLabel.text = mRoomData.ItemName + " : " + mRoomData.ItemCount;
		generateSpeedLabel.text = (mRoomData.GenerateSpeed * mRoomData.ItemCount) + " / \u79d2";
	}

	private void Reset () {
		DialogController.itemBoughtEvent -= itemBoughtEvent;
		DialogController.dialogClosedEvent -= dialogClosedEvent;
		buyButton.transform.eulerAngles = new Vector3 (0, 0, 45);
		iTweenEvent comeBackEvent = iTweenEvent.GetEvent (buyButtonParent, "ComeBackEvent");
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

	private void GenerateResident (int count) {
		for (int i = 0; i<count; i++) {
			float x = Random.Range (-200, 200);
			float y = Random.Range (-160, -80);
			GameObject resident = Instantiate (residentPrefab) as GameObject;
			resident.transform.parent = transform.parent;
			resident.transform.localPosition = new Vector3 (x, y, 0);
		}
	}
}
