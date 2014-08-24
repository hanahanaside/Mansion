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
	public UIGrid[] itemGridArray;
	private RoomData mRoomData;
	private int mMaxItemCount;

	void Start () {
		foreach (UIGrid grid in itemGridArray) {
			List<Transform> itemList = grid.GetChildList ();
			mMaxItemCount += itemList.Count;
		}
	}
	
	void Init (RoomData roomData) {
		mRoomData = roomData;
		if (mRoomData.ItemCount == 0) {
			lockObject.SetActive (true);
			return;
		}
		SetActiveItem ();
		GenerateResident (mRoomData.ItemCount);
		SetTextData ();
	}

	void itemBoughtEvent () {
		Debug.Log ("boughtEvent");
		if (mRoomData.ItemCount == 0) {
			lockObject.SetActive (false);
		}
		mRoomData.ItemCount++;
		RoomDataDao.Instance.UpdateItemCount (mRoomData);
		CountManager.Instance.DecreaseMoneyCount (mRoomData.ItemPrice);
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
		SoundManager.Instance.PlaySE (AudioClipID.SE_BUTTON);
		ShowRoomItemDialog ();
		iTweenEvent removeEvent = iTweenEvent.GetEvent (buyButtonParent, "ExitEvent");
		removeEvent.Play ();
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

	private void SetActiveItem () {
		if (mRoomData.ItemCount > mMaxItemCount) {
			return;
		}
		foreach (UIGrid grid in itemGridArray) {
			List<Transform> itemList = grid.GetChildList ();
			for (int i = 0; i < mRoomData.ItemCount; i++) {
				GameObject item = itemList [i].gameObject;
				UISprite sprite = item.GetComponent<UISprite> ();
				if (!sprite.enabled) {
					sprite.enabled = true;
					return;
				}
			}
		}
	}

	private void GenerateResident (int count) {
		for (int i = 0; i<count; i++) {
			float x = Random.Range (-200, 200);
			float y = Random.Range (-100, 50);
			GameObject resident = Instantiate (residentPrefab) as GameObject;
			resident.transform.parent = transform.parent;
			resident.transform.localScale = new Vector3 (1, 1, 1);
			resident.transform.localPosition = new Vector3 (x, y, 0);
		}
	}
}
