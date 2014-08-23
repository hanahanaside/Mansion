using UnityEngine;
using System.Collections;

public class SpaceVikingController : EnemyController {

	public UISprite sprite;
	public UIAtlas charactorAtlas;
	public iTweenEvent[] atackEventArray;
	
	// Update is called once per frame
	void Update () {
		if (IsAtacking) {
			return;
		}
		AtackIntervalTime -= Time.deltaTime;
		if (AtackIntervalTime < 0) {
			StartCoroutine (Atack ());
		} else {
			Walk ();
		}
	}
	
	public override IEnumerator Atack () {
		Debug.Log ("atack");
		IsAtacking = true;
		SetSprite("enemy6_atack_1");
		yield return new WaitForSeconds (1.0f);
		SetSprite("enemy6_atack_2");
		foreach(iTweenEvent atackEvent in atackEventArray){
			atackEvent.Play();
		}
		yield return new WaitForSeconds (2.0f);
		foreach(iTweenEvent atackEvent in atackEventArray){
			atackEvent.Stop();
		}
		yield return new WaitForSeconds (0.5f);
		SetSprite("enemy6_walk_1");
		SetAtackIntervalTime ();
		IsAtacking = false;
	}
	
	private void SetSprite(string spriteName){
		sprite.spriteName = spriteName;
		float height = charactorAtlas.GetSprite(spriteName).height;
		float width = charactorAtlas.GetSprite(spriteName).width;
		sprite.width = (int)width;
		sprite.height = (int)height;
	}

}
