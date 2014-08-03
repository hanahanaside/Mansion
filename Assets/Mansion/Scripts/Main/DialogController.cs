using UnityEngine;
using System;
using System.Collections;

public class DialogController : MonoBehaviour {

	public static event Action itemBoughtEvent;
	public static event Action dialogClosedEvent;

	public virtual void OnBuyButtonClicked () {
		itemBoughtEvent ();
	}
	
	public virtual void OnCloseButonClicked () {
		dialogClosedEvent ();
	}
}
