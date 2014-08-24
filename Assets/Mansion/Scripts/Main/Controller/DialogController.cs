using UnityEngine;
using System;
using System.Collections;

public class DialogController : MonoBehaviour {

	public static event Action itemBoughtEvent;
	public static event Action dialogClosedEvent;

	void Start () {
		FenceManager.Instance.ShowFence ();
	}

	public virtual void OnBuyButtonClicked () {
		SoundManager.Instance.PlaySE(AudioClipID.SE_BUTTON);
		itemBoughtEvent ();
		FenceManager.Instance.HideFence ();
	}
	
	public virtual void OnCloseButonClicked () {
		SoundManager.Instance.PlaySE(AudioClipID.SE_BUTTON);
		dialogClosedEvent ();
		FenceManager.Instance.HideFence ();
	}
}
