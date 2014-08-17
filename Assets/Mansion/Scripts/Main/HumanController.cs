using UnityEngine;
using System.Collections;

public class HumanController : MonoBehaviour {

	public UISprite sprite;
	public UIAtlas roomItemAtlas;
	private float walkSpeed = -0.001f;

	// Update is called once per frame
	void Update () {
		string spriteName = sprite.spriteName;
		UISpriteData spriteData = roomItemAtlas.GetSprite(spriteName);
		sprite.width = spriteData.width;
		sprite.height = spriteData.height;
		float x = transform.localPosition.x;
		if (x < -280) {
			transform.eulerAngles = new Vector3(0,180,0);
		}
		if (x > 280) {
			transform.eulerAngles = new Vector3(0,0,0);
		}
		transform.Translate (walkSpeed, 0, 0);
	}
}
