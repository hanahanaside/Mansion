using UnityEngine;
using System.Runtime.InteropServices;

public class Binding {
    [DllImport("__Internal")]
	
	private static extern void ChkAppListView_ ();

     
    public static void ChkAppListView () {
        if (Application.platform != RuntimePlatform.OSXEditor) {
            ChkAppListView_();
			PlayerPrefs.SetString("ChkAppListView","value");
        }
    }  
}
