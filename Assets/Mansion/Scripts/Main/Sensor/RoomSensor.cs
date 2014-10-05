using UnityEngine;
using System.Collections;

public class RoomSensor : MonoBehaviour {
		public UISprite sprite;

	void Update(){
		//	rigidbody.WakeUp ();
	}

	void  OnTriggerEnter (Collider collider) {
		Debug.Log ("aaaa");
			ChangeDepth (collider);
	}

	void OnTriggerExit(Collider collider){

		//		ChangeDepth (collider);
	}

	void OnTriggerStay(Collider collider){

	}
		
	private void ChangeDepth(Collider collider){
		string tag = collider.tag;
		if (tag == "Resident") {
			collider.gameObject.SendMessage ("MoveDepth", sprite);
		}
		if(tag == "CharactorSensor"){
			collider.gameObject.transform.parent.SendMessage ("MoveDepth", sprite);
		}
	}
}
