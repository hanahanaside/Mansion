using UnityEngine;
using System.Collections;

public class ShopPanelController : MonoBehaviour {

	public GameObject shopItemDialogPrefab;
	public UILabel secomCountLabel;

	public void OnSecomButtonClicked () {
		SoundManager.Instance.PlaySE (AudioClipID.SE_BUTTON);
		SecomData secomData = PrefsManager.Instance.GetSecomData ();
		if(secomData.Count >= 10){
			return;
		}
		ShopItemData shopItemData = new ShopItemData ();
		shopItemData.Description = secomData.Description;
		shopItemData.Price = secomData.Price;
		shopItemData.Tag = ShopItemData.TAG_SECOM;
		GameObject shopItemDialog = Instantiate (shopItemDialogPrefab) as GameObject;
		shopItemDialog.transform.parent = UIRootInstanceKeeper.UIRootGameObject.transform;
		shopItemDialog.transform.localScale = new Vector3 (1, 1, 1);
		shopItemDialog.BroadcastMessage ("Init", shopItemData);
		DialogController.itemBoughtEvent += itemBoughtEvent;
		DialogController.dialogClosedEvent += dialogClosedEvent;
	}

	public void itemBoughtEvent () {
		SecomData secomData = PrefsManager.Instance.GetSecomData ();
		secomData.Count++;
		PrefsManager.Instance.SaveSecomData (secomData);
		secomCountLabel.text = "×" + secomData.Count;
	}

	public void dialogClosedEvent () {
		RemoveEvents ();
	}

	private void RemoveEvents () {
		DialogController.itemBoughtEvent -= itemBoughtEvent;
		DialogController.dialogClosedEvent -= dialogClosedEvent;
	}

}
