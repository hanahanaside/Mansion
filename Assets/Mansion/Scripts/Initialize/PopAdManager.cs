using UnityEngine;
using System.Collections;

public class PopAdManager : MonoBehaviour {

	public static PopAdManager sInstance;

	void Start () {
		sInstance = this;
		DontDestroyOnLoad (gameObject);
		#if !UNITY_EDITOR
		Binding.SplashViewInitialize ();
		#endif
	}

	public static PopAdManager Instance{
		get{
			return sInstance;
		}
	}

	public void ShowPopAd(){
		#if !UNITY_EDITOR
		Binding2.SplashView ();
		#endif
	}
}
