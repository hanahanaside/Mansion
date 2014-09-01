using UnityEngine;
using System.Collections;

public class ShopPanelController : MonoBehaviour {

	public GameObject shopItemDialogPrefab;
	public GameObject reviewDialogPrefab;
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
		GameObject shopItemDialog = ShowDialog (shopItemDialogPrefab);
		shopItemDialog.BroadcastMessage ("Init", shopItemData);
		DialogController.itemBoughtEvent += itemBoughtEvent;
		DialogController.dialogClosedEvent += dialogClosedEvent;
	}

	public void OnReviewButtonClicked(){
		SoundManager.Instance.PlaySE (AudioClipID.SE_BUTTON);
		GameObject reviewDialog = ShowDialog (reviewDialogPrefab);
	}

	public void OnShareButtonClicked(){
		SoundManager.Instance.PlaySE (AudioClipID.SE_BUTTON);
	}

	public void itemBoughtEvent () {
		SecomData secomData = PrefsManager.Instance.GetSecomData ();
		CountManager.Instance.DecreaseMoneyCount (secomData.Price);
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

	private GameObject ShowDialog(GameObject dialogPrefab){
		GameObject dialog = Instantiate (dialogPrefab) as GameObject;
		dialog.transform.parent = UIRootInstanceKeeper.UIRootGameObject.transform;
		dialog.transform.localScale = new Vector3 (1, 1, 1);
		return dialog;
	}
}
