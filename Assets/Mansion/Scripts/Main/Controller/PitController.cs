using UnityEngine;
using System.Collections;

public class PitController : MonoBehaviour {

	public UIButton pitButton;
	public UISprite backGroundSprite;
	public GameObject countLabelPrefab;
	public GameObject moneyParticlePrefab;
	public GameObject pit;
	private int mPitLevel;
	
	void Init (int pitLevel) {
		Debug.Log("pitLevel = " + pitLevel);
		mPitLevel = pitLevel;
		backGroundSprite.spriteName = "bg_pit_"+mPitLevel;
		pitButton.normalSprite = "pit_" + mPitLevel;
	}

	public void OnPitClicked () {
		SoundManager.Instance.PlaySE (AudioClipID.SE_MONEY);
		SoundManager.Instance.PlaySE (AudioClipID.SE_DIG);

		GameObject countLabelObject = InstantiateObject (countLabelPrefab);
		countLabelObject.transform.Translate (0, 0.3f, 0);
		int getPoint = CalcGetPoint ();
		countLabelObject.SendMessage ("SetCount", getPoint);
		InstantiateObject (moneyParticlePrefab);
		CountManager.Instance.AddGeneratedCount (getPoint);
	}

	private GameObject InstantiateObject (GameObject prefab) {
		GameObject instantiateObject = Instantiate (prefab)as GameObject;
		instantiateObject.transform.parent = transform.parent;
		instantiateObject.transform.localPosition = pit.transform.localPosition;
		instantiateObject.transform.localScale = new Vector3 (1, 1, 1);
		return instantiateObject;
	}

	private int CalcGetPoint () {
		int getPoint = 0;
		switch (mPitLevel) {
		case 1:
			getPoint = 100;
			break;
		case 2:
			getPoint = 200;
			break;
		case 3:
			getPoint = 300;
			break;
		case 4:
			getPoint = 400;
			break;
		case 5:
			getPoint = 500;
			break;
		}
		return getPoint;
	}
}
