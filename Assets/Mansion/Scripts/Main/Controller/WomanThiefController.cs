using UnityEngine;
using System.Collections;

public class WomanThiefController : EnemyController {

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
