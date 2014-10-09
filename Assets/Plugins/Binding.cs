using UnityEngine;
using System.Runtime.InteropServices;

public class Binding {
    [DllImport("__Internal")]
	
	private static extern void SplashViewInitialize_ ();
     
	public static void SplashViewInitialize () {
        if (Application.platform != RuntimePlatform.OSXEditor) {
			SplashViewInitialize_();
			PlayerPrefs.SetString("SplashViewInitialize","value");
        }
    }  

	[DllImport("__Internal")]

	private static extern void ChkAppListView_ ();


	public static void ChkAppListView () {
		if (Application.platform != RuntimePlatform.OSXEditor) {
			ChkAppListView_();
			PlayerPrefs.SetString("ChkAppListView","value");
		}
	}  
	
}

public class Binding2 {
	[DllImport("__Internal")]

	private static extern void SplashView_ ();

	
	public static void SplashView () {
		if (Application.platform != RuntimePlatform.OSXEditor) {
			SplashView_();
			PlayerPrefs.SetString("SplashView","value");
		}
	}  
}