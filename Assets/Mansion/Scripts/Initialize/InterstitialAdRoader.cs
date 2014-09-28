using UnityEngine;
using System.Collections;

public class InterstitialAdRoader : MonoBehaviour {

	public Account iOS;
	public Account Android;

	// Use this for initialization
	void Start () {
		#if !UNITY_EDITOR
		Load();
		#endif
	}

	private void Load(){
		#if UNITY_IPHONE
		NendAdInterstitial.Instance.Load (iOS.apiKey,iOS.spotId);
		#endif

		#if UNITY_ANDROID
		NendAdInterstitial.Instance.Load (Android.apiKey,Android.spotId);
		#endif
	}

	[System.SerializableAttribute]
	public class Account{
		public string apiKey;
		public string spotId;
	}
}
