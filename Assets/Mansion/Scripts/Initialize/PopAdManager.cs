using UnityEngine;
using System.Collections;

public class PopAdManager : MonoBehaviour {

	public static PopAdManager sInstance;

	void Start () {
		sInstance = this;
		DontDestroyOnLoad (gameObject);
		#if UNITY_IPHONE
		Binding.SplashViewInitialize ();
		#endif
	}

	public static PopAdManager Instance{
		get{
			return sInstance;
		}
	}

	public void ShowPopAd(){
		#if UNITY_IPHONE
		Binding2.SplashView ();
		#endif
	}
}
