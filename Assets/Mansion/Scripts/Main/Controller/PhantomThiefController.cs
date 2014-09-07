using UnityEngine;
using System.Collections;

public class PhantomThiefController : EnemyController {

	public UISprite sprite;
	public UIAtlas charactorAtlas;
	public iTweenEvent atackEvent;

	// Update is called once per frame
	void Update () {
		if (IsAtacking) {
			return;
		}
		AtackIntervalTime -= Time.deltaTime;
		if (AtackIntervalTime < 0) {
			StartAtacking ();
		} else {
			Walk ();
		}
	}
	
	public override IEnumerator Atack () {
		Debug.Log ("atack");
		IsAtacking = true;
		SetSprite("enemy5_atack_1");
		yield return new WaitForSeconds (1.0f);
		SetSprite("enemy5_atack_2");
		float y = transform.localRotation.eulerAngles.y;
		transform.localRotation = Quaternion.Euler(0,y,10);
		atackEvent.Play();
		ApplyDamage();
		yield return new WaitForSeconds (2.0f);
		atackEvent.Stop();
		yield return new WaitForSeconds (0.5f);
		transform.localRotation = Quaternion.Euler(0,0,0);
		SetSprite("enemy5_walk_1");
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
