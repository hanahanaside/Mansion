using UnityEngine;
using System.Collections;

public class RoomSensor : MonoBehaviour {
		public UISprite sprite;

	void Update(){
		//	rigidbody.WakeUp ();
	}

	void  OnTriggerEnter (Collider collider) {
			ChangeDepth (collider);
	}

	void OnTriggerExit(Collider collider){

				ChangeDepth (collider);
	}

	void OnTriggerStay(Collider collider){
		//	ChangeDepth (collider);
	}

	void Show(){
		collider.enabled = true;

	}

	private void ChangeDepth(Collider collider){
		string tag = collider.tag;
		if (tag == "Resident" || tag == "Enemy") {
//			if(sprite.color == Color.red){
//				sprite.color = Color.blue;
//			}else {
//				sprite.color = Color.red;
//			}
			collider.gameObject.SendMessage ("MoveDepth", sprite);
		}

	}
}
