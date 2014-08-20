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
		string baseFilePath = "";
		string filePath = "";

		#if UNITY_IPHONE
		baseFilePath = Application.streamingAssetsPath + "/" + DATABASE_FILE_NAME;
		filePath = Application.persistentDataPath + "/" + DATABASE_FILE_NAME;
		File.Delete(filePath);
		if(!File.Exists(filePath)){
			File.Copy( baseFilePath, filePath); 
		}
		createdDatabaseEvent ();

		#endif

		#if UNITY_ANDROID
		baseFilePath = Path.Combine (Application.streamingAssetsPath, DATABASE_FILE_NAME);
		#if UNITY_EDITOR
		baseFilePath = "file://"+Path.Combine (Application.streamingAssetsPath, DATABASE_FILE_NAME);
		#endif
		filePath = Application.persistentDataPath + "/" + DATABASE_FILE_NAME;
		File.Delete(filePath);
		if(File.Exists(filePath)){
			createdDatabaseEvent ();
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
