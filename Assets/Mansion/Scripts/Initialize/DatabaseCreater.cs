using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class DatabaseCreater : MonoBehaviour {

	public static event Action createdDatabaseEvent;

	private const string DATABASE_FILE_NAME = "manshon.db";
	
	void Start () {
		CreateDatabase ();
	}
	
	// Use this for initialization
	private void CreateDatabase () {
		Debug.Log("create Database");
		string baseFilePath = Application.streamingAssetsPath + "/" + DATABASE_FILE_NAME;
		string filePath = Application.persistentDataPath + "/" + DATABASE_FILE_NAME;
#if UNITY_EDITOR
		File.Delete(filePath);
		PrefsManager.Instance.SaveMoneyCount(0);
		SecomData secomData = new SecomData();
		secomData.Count = 0;
		secomData.MacxCount = 0;
		PrefsManager.Instance.SaveSecomData(secomData);
#endif

		#if UNITY_IPHONE
		if(!File.Exists(filePath)){
			File.Copy( baseFilePath, filePath); 
			Debug.Log("create Database");
		}
		createdDatabaseEvent();
		#endif
		
		#if UNITY_ANDROID 
		#if UNITY_EDITOR
		baseFilePath = "file://"+Path.Combine (Application.streamingAssetsPath, DATABASE_FILE_NAME);
		#endif
		if(File.Exists(filePath)){
			createdDatabaseEvent();
		}else {
			StartCoroutine(CreateAndroidDatabase(baseFilePath,filePath));
		}
		#endif
	}

	#if UNITY_ANDROID
	private IEnumerator CreateAndroidDatabase (string baseFilePath, string filePath) {

		Debug.Log ("CreateAndroidDatabase");
		Debug.Log ("baseFilePath = " + baseFilePath);
		Debug.Log ("filePath = " + filePath);
		WWW www = new WWW (baseFilePath);
		yield return www;
		File.WriteAllBytes (filePath, www.bytes);
		createdDatabaseEvent ();

	}
	#endif

}
