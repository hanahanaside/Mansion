using UnityEngine;
using System.Collections;

public class CatController : EnemyController {

	// Update is called once per frame
	void Update () {
		if (IsAtacking) {
			return;
		}
		AtackIntervalTime -= Time.deltaTime;
		if (AtackIntervalTime < 0) {
			StopWalkAnimation ();
			StartAtacking ();
		} else {
			Walk ();
		}
	}

	void MoveDepth(UISprite sprite){

		Vector3 roomPosition = sprite.transform.position;
		float roomY = roomPosition.y + 0.1f;
		float residentY = transform.position.y;
		if(residentY > roomY){
			this.sprite.depth = sprite.depth - 1;
		}else {
			this.sprite.depth = sprite.depth + 1;
		}
	}


	public override IEnumerator Atack () {
		Debug.Log ("atack");
		IsAtacking = true;
		atackAnimation.enabled = true;
		ApplyDamage();
		yield return new WaitForSeconds (2.0f);
		atackAnimation.enabled = false;
		yield return new WaitForSeconds (1.0f);
		SetAtackIntervalTime ();
		IsAtacking = false;
		RestartWalkAnimation ();
	}

}
