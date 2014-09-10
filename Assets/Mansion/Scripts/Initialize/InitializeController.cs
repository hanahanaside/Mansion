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
		Debug.Log ("Database created");

		if (PrefsManager.Instance.FlagOpeningFinished == 0) {
			Application.LoadLevel ("Opening");
		} else {
			Application.LoadLevel ("Main");
		}
	}
}
