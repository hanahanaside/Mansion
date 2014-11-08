using UnityEngine;
using System.Collections;

public class  adcropsObserver : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI()
	{
		Rect rect = new Rect(10, 10, 400, 100);
		bool isClicked = GUI.Button(rect, "adcrops");
		if (isClicked)
		{
			showAdcrops();
		}
	}
	

	void showAdcrops() {
		#if UNITY_ANDROID
		using (AndroidJavaClass cls_UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {

            using (AndroidJavaObject obj_Activity = cls_UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity")) {

                obj_Activity .CallStatic("adcrops");
			}
		}
		#endif
    }

	
}