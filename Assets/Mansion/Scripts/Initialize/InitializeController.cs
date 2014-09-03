using UnityEngine;
using System.Collections;

public class InitializeController : MonoBehaviour {

	void OnEnable(){
		DatabaseCreater.createdDatabaseEvent += OnDatabaseCreatedEvent;
	}

	void OnDisable(){
		DatabaseCreater.createdDatabaseEvent -= OnDatabaseCreatedEvent;
	}

	void OnDatabaseCreatedEvent(){
		Debug.Log("Database created");
		Application.LoadLevel("Opening");
	}
}
