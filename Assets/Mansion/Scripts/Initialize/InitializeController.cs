using UnityEngine;
using System.Collections;

public class InitializeController : MonoBehaviour {
	void OnEnable () {
		DatabaseCreater.createdDatabaseEvent += OnDatabaseCreatedEvent;
	}

	void OnDisable () {
		DatabaseCreater.createdDatabaseEvent -= OnDatabaseCreatedEvent;
	}

	void OnDatabaseCreatedEvent () {
		Debug.Log ("OnDatabaseCreatedEvent");
		if (PrefsManager.Instance.FlagTutorialFinished == 0) {
			Application.LoadLevel ("Opening");
		} else {
			Application.LoadLevel ("Main");
		}
	}
}
