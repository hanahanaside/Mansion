using UnityEngine;
using System.Collections;
using System;

public abstract class EnemyController : HumanController {

	public UISpriteAnimation atackAnimation;
	private GameObject mDamageLabelPrefab;
	private GameObject mGetMoneyLabelPrefab;
	private EnemyData mEnemyData;
	private long mTotalDamage;

	void Start () {
		SetAtackIntervalTime ();
		mDamageLabelPrefab = Resources.Load("Prefabs/Effect/DamageLabel") as GameObject;
		mGetMoneyLabelPrefab =  Resources.Load("Prefabs/Effect/GetMoneyLabel") as GameObject;
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
		int getMoneyCount = mEnemyData.Atack;
		GameObject getMoneyLabelObject = Instantiate(mGetMoneyLabelPrefab) as GameObject;
		getMoneyLabelObject.transform.parent = transform.parent;
		getMoneyLabelObject.transform.localScale = new Vector3(1,1,1);
		getMoneyLabelObject.transform.localPosition = transform.localPosition;
		getMoneyLabelObject.SendMessage("SetCount",getMoneyCount);
		CountManager.Instance.AddMoneyCount(getMoneyCount);
		StatusDataKeeper.Instance.IncrementAtackEnemyCount ();

		//ヒストリーデータをインサート
		InsertHistoryData ();
		Destroy (gameObject);
	}
	
	public void SetAtackIntervalTime () {
		AtackIntervalTime = 5.0f;
	}

	public void ApplyDamage(){
		int damage = mEnemyData.Atack;
		mTotalDamage += damage;
		StatusDataKeeper.Instance.AddDamagedCount (damage);
		GameObject damageLabelObject = Instantiate(mDamageLabelPrefab)as GameObject;
		damageLabelObject.transform.parent = transform.parent;
		damageLabelObject.transform.localScale = new Vector3(1,1,1);
		damageLabelObject.transform.localPosition = transform.localPosition;
		damageLabelObject.SendMessage("SetCount",damage);
		CountManager.Instance.DecreaseMoneyCount(damage);
	}

	private void InsertHistoryData(){
		DateTime dtNow = DateTime.Now;
		string date = dtNow.ToString ("MM/dd HH:mm");
		string damage = mTotalDamage.ToString ();
		HistoryData historyData = new HistoryData ();
		historyData.EnemyId = mEnemyData.Id;
		historyData.Damage = DamageConverter.Convert(damage);
		historyData.Date = date;
		HistoryDataDao.Instance.InsertHistoryData (historyData);
	}
}
