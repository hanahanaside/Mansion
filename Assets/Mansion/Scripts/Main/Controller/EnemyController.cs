using UnityEngine;
using System.Collections;

public class EnemyController : HumanController {

	public UISpriteAnimation atackAnimation;
	private float mAtackIntervalTime;
	private bool mIsAtacking;
	private EnemyData mEnemyData;

	void Start(){
		SetAtackIntervalTime();
	}

	void Update () {
		if(mIsAtacking){
			return;
		}
		mAtackIntervalTime -= Time.deltaTime;
		if (mAtackIntervalTime < 0) {
			StopWalkAnimation();
			StartCoroutine(Atack());
		} else {
			Walk ();
		}
	}

	void SetEnemyData(EnemyData enemyData){
		mEnemyData = enemyData;
	}

	public void OnClick(){
		Debug.Log("clicked");
		Destroy(gameObject);
	}

	private IEnumerator Atack () {
		Debug.Log("atack");
		mIsAtacking = true;
		atackAnimation.enabled = true;
		yield return new WaitForSeconds(2.0f);
		atackAnimation.enabled = false;
		yield return new WaitForSeconds(1.0f);
		SetAtackIntervalTime();
		mIsAtacking = false;
		RestartWalkAnimation();
	}

	private void SetAtackIntervalTime(){
		mAtackIntervalTime = 5.0f;
	}
}
