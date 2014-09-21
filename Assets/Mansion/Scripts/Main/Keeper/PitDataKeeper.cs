using UnityEngine;
using System.Collections;

public class PitDataKeeper : MonoBehaviour {
	private static PitDataKeeper sInstance;
	private ShopItemData mPitData;

	void Awake () {
		sInstance = this;
		UpdatePitData ();
	}

	public static PitDataKeeper Instance {
		get {
			return sInstance;
		}
	}

	public ShopItemData PitData {
		get {
			return mPitData;
		}
	}

	public void UpdatePitData () {
		mPitData = ShopItemDataDao.Instance.GetPitData ();
	}
	//ピットのレベルによるボーナス倍率を返す
	public decimal GetPitBonusTimes () {
		decimal pitBonusTimes = 0;
		switch (mPitData.Id) {
		case 1:
			pitBonusTimes = 1m;
			break;
		case 2:
			pitBonusTimes = 1.1m;
			break;
		case 3:
			pitBonusTimes = 1.2m;
			break;
		case 4:
			pitBonusTimes = 1.25m;
			break;
		case 5:
			pitBonusTimes = 1.3m;
			break;
		}
		return pitBonusTimes;
	}
}
