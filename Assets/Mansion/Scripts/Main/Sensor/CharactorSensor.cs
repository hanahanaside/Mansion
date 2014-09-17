using UnityEngine;
using System.Collections;

public class CharactorSensor : MonoBehaviour {

	public UISprite sprite;
	public BoxCollider boxCollider;

	void Start () {
		int width = sprite.width;
		boxCollider.size = new Vector3 (width, 0.5f, 0);
	}

}
