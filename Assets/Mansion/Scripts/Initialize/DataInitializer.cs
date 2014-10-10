using UnityEngine;
using System.Collections;
using System.IO;

public class DataInitializer : MonoBehaviour {

	#if UNITY_EDITOR
	private const string DATABASE_FILE_NAME = "manshon.db";

	void Awake(){
		string filePath = Application.persistentDataPath + "/" + DATABASE_FILE_NAME;
		File.Delete(filePath);
		PrefsManager.Instance.SaveMoneyCount(2000);
		SecomData secomData = new SecomData();
		secomData.Count = 0;
		secomData.MacxCount = 0;
		PrefsManager.Instance.SaveSecomData(secomData);
		PrefsManager.Instance.FlagTutorialFinished = 0;
		PrefsManager.Instance.SaveMoneyCount (1000000000000m);
	}
	#endif
}
