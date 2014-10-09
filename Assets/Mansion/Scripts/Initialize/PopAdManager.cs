using UnityEngine;
using System.Collections;

public class PopAdManager : MonoBehaviour {

	public static PopAdManager sInstance;

	void Start () {
		sInstance = this;
		DontDestroyOnLoad (gameObject);
		Binding.SplashViewInitialize ();
	}

	public static PopAdManager Instance{
		get{
			return sInstance;
		}
	}

	public void ShowPopAd(){
		Binding2.SplashView ();
	}
}
