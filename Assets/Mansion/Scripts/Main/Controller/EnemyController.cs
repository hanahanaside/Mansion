using UnityEngine;
using System.Collections;

public abstract class EnemyController : HumanController {

	public UISpriteAnimation atackAnimation;
	public int enemyId;
	private GameObject mDamageLabelPrefab;
	private EnemyData mEnemyData;

	void Start () {
		SetAtackIntervalTime ();
		mDamageLabelPrefab = Resources.Load("Prefabs/Effect/DamageCountLabel") as GameObject;
		mEnemyData = EnemyDataDao.Instance.QueryEnemyData(enemyId);
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
		SoundManager.Instance.PlaySE(AudioClipID.SE_ATACK);
		SoundManager.Instance.StopBGM();
		SoundManager.Instance.PlayBGM(AudioClipID.BGM_MAIN);
		EnemyGenerator.Instance.AttackedEnemy();
		Destroy (gameObject);
	}
	
	public void SetAtackIntervalTime () {
		AtackIntervalTime = 5.0f;
	}

	public void ApplyDamage(){
		GameObject damageLabelObject = Instantiate(mDamageLabelPrefab)as GameObject;
		damageLabelObject.transform.parent = transform.parent;
		damageLabelObject.transform.localScale = new Vector3(1,1,1);
		damageLabelObject.transform.localPosition = transform.localPosition;
		damageLabelObject.SendMessage("SetCount",mEnemyData.Atack);
	}
}
