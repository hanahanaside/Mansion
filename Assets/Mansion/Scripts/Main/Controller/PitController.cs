using UnityEngine;
using System.Collections;

public class PitController : MonoBehaviour {
	public UISprite backGroundSprite;
	public GameObject countLabelPrefab;
	public GameObject moneyParticlePrefab;
	public GameObject pit;
	public GameObject effectPoint;
	private ShopItemData mCurrentPitData;
	private ShopItemData mNextPitData;

	void Init (ShopItemData pitData) {
		mCurrentPitData = pitData;
		if (mCurrentPitData.Id < 5) {
			mNextPitData = ShopItemDataDao.Instance.GetShopItemDataById (mCurrentPitData.Id + 1);
		}
		Debug.Log ("pitLevel = " + mCurrentPitData.Id);
		backGroundSprite.spriteName = "bg_pit_" + mCurrentPitData.Id;
		string spriteName = "pit_" + mCurrentPitData.Id;
		pit.GetComponent<UIButton> ().normalSprite = spriteName;
		pit.GetComponent<UISprite> ().spriteName = spriteName;
	}

	void A(){
		Debug.Log ("aaaaaaaaaaaaaaaaaaa");
	}

	public void OnPitClicked () {
		SoundManager.Instance.PlaySE (AudioClipID.SE_MONEY);
		SoundManager.Instance.PlaySE (AudioClipID.SE_DIG);
		StatusDataKeeper.Instance.IncrementTotalTapPitCount ();
		GameObject countLabelObject = InstantiateObject (countLabelPrefab);
		countLabelObject.transform.Translate (0, 0.02f, 0);
		int getPoint = mCurrentPitData.Effect;
		countLabelObject.SendMessage ("SetCount", "+" + getPoint);
		GameObject effectObject = InstantiateObject (moneyParticlePrefab);
		effectObject.transform.Translate (0,-0.2f,0);
		CountManager.Instance.AddMoneyCount (getPoint);
		StatusDataKeeper.Instance.AddTotalPitGenerateCount (getPoint);
		Debug.Log ("total tap count = " + StatusDataKeeper.Instance.StatusData.TotalTapPitCount);
		CheckUnLickPitItem ();
	}

	private void CheckUnLickPitItem () {
		if (mCurrentPitData.Id >= 5) {
			return;
		}
		if (mNextPitData.UnlockLevel == ShopItemData.UNLOCK_LEVEL_UNLOCKED) {
			return;
		}
		if (StatusDataKeeper.Instance.StatusData.TotalTapPitCount >= mNextPitData.UnLockCondition) {
			Debug.Log ("unlock pit");
			ShopItemDataDao.Instance.UpdateUnLockLevel (mCurrentPitData.Id + 1, ShopItemData.UNLOCK_LEVEL_UNLOCKED);
		}
	}

	private GameObject InstantiateObject (GameObject prefab) {
		GameObject instantiateObject = Instantiate (prefab)as GameObject;
		instantiateObject.transform.parent = transform.parent;
		instantiateObject.transform.localPosition = pit.transform.localPosition;
		instantiateObject.transform.localScale = new Vector3 (1, 1, 1);
		return instantiateObject;
	}
}
