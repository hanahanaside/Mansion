using UnityEngine;
using System.Collections;

public class InterstitialAdRoader : MonoBehaviour {

	public string apiKey;
	public string spotId;

	// Use this for initialization
	void Start () {
		NendAdInterstitial.Instance.Load (apiKey,spotId);
	}

}
