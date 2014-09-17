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
	}
	
	public virtual void OnCloseButonClicked () {
		SoundManager.Instance.PlaySE(AudioClipID.SE_BUTTON);
		if(dialogClosedEvent != null){
			dialogClosedEvent ();
		}
		FenceManager.Instance.HideFence ();
	}

	public void FirstItemBought(){
		itemBoughtEvent ();
		SoundManager.Instance.PlaySE (AudioClipID.SE_ADD_APART);
		dialogClosedEvent ();
		FenceManager.Instance.HideFence ();
	}

	public void OnBuyApart(){
		SoundManager.Instance.PlaySE (AudioClipID.SE_ADD_APART);
		itemBoughtEvent ();
	}

	public void OnBuyShopItem(){
		SoundManager.Instance.PlaySE (AudioClipID.SE_GET_SHOP_ITEM);
		itemBoughtEvent ();
	}
}
