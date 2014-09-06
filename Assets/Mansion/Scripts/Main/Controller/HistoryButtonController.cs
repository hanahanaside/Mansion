﻿using UnityEngine;
using System.Collections;

public class HistoryButtonController : MonoBehaviour {

	public UILabel dateLabel;
	public UILabel damageLabel;
	private GameObject mHistoryImageObject;

	void OnDisable(){
		Destroy (mHistoryImageObject);
	}

	void Init (HistoryData historydata) {
		dateLabel.text = historydata.Date;
		GameObject historyImagePrefab =  (Resources.Load ("Prefabs/Button/HistoryImage_" + historydata.EnemyId)) as GameObject;
		mHistoryImageObject =  Instantiate (historyImagePrefab) as GameObject;
		mHistoryImageObject.transform.parent = transform.parent;
		mHistoryImageObject.transform.localScale = new Vector3 (1,1,1);
		mHistoryImageObject.transform.localPosition = new Vector3 (0,0,0);
		switch(historydata.FlagSecom){
		case 0:
			damageLabel.text = historydata.Damage;
			mHistoryImageObject.BroadcastMessage ("HideSecuritySprite");
			break;
		case 1:
			damageLabel.text = "\u30a2\u30eb\u30c4\u30c3\u30af発動";
			mHistoryImageObject.BroadcastMessage ("ShowSecuritySprite");
			break;
		}
	}
}
