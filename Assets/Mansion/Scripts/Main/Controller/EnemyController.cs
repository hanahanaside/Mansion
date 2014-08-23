using UnityEngine;
using System.Collections;

public abstract class EnemyController : HumanController {

	public UISpriteAnimation atackAnimation;
	private EnemyData mEnemyData;

	void Start () {
		SetAtackIntervalTime ();
	}

	void SetEnemyData (EnemyData enemyData) {
		mEnemyData = enemyData;
	}

	public float AtackIntervalTime{
		set;
		get;
	}

	public bool IsAtacking {
		set;
		get;
	}

	public abstract IEnumerator Atack();

	public void OnClick () {
		Debug.Log ("clicked");
		EnemyGenerator.Instance.EXSpriteEnabled(false);
		Destroy (gameObject);
	}
	
	public void SetAtackIntervalTime () {
		AtackIntervalTime = 5.0f;
	}

	public void ApplyDamage(EnemyData enemyData){

	}
}
