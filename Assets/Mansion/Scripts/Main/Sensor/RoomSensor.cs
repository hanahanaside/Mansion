using UnityEngine;
using System.Collections;

public class RoomSensor : MonoBehaviour {
	public UISprite sprite;
	public BoxCollider boxCollider;

	void Start () {
		int width = sprite.width;
		boxCollider.size = new Vector3 (width, sprite.height, 0);
	}

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
		Debug.Log ("stay");
			ChangeDepth (collider);
	}

	void Show(){
		boxCollider.enabled = true;
	}

	private void ChangeDepth(Collider collider){
		string tag = collider.tag;
		if (tag == "CharactorSensor") {
//			if(sprite.color == Color.red){
//				sprite.color = Color.blue;
//			}else {
//				sprite.color = Color.red;
//			}
			GameObject parentObject = collider.gameObject.transform.parent.gameObject;
			parentObject.BroadcastMessage ("MoveDepth", sprite);
		}

	}
}
