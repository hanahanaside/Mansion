using UnityEngine;
using System.Collections;

public class ShopButtonController : MonoBehaviour {

	public GameObject shopDialogPrefab;
	private ShopItemData mShopItemData;

	void Init(ShopItemData shopItemData){
		mShopItemData = shopItemData;
	}
	
	public void OnButtonClicked () {
		GameObject uiRoot = UIRootInstanceKeeper.UIRootGameObject;
		GameObject shopDialog = Instantiate (shopDialogPrefab) as GameObject;
		shopDialog.transform.parent = uiRoot.transform;
		shopDialog.transform.localScale = new Vector3 (1, 1, 1);
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
	}
}
