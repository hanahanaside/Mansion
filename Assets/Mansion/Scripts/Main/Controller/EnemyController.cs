using UnityEngine;
using System.Collections;
using System;

public abstract class EnemyController : HumanController {
	public UISpriteAnimation atackAnimation;
	public UISpriteAnimation destroyAnimation;
	private GameObject mDamageLabelPrefab;
	private GameObject mGetMoneyLabelPrefab;
	private EnemyData mEnemyData;
	private decimal mTotalDamage;

	void Start () {
		SetAtackIntervalTime ();
		mDamageLabelPrefab = Resources.Load ("Prefabs/Effect/DamageLabel") as GameObject;
		mGetMoneyLabelPrefab = Resources.Load ("Prefabs/Effect/GetMoneyLabel") as GameObject;
	}

	void SetEnemyData (EnemyData enemyData) {
		mEnemyData = enemyData;
	}

	void Init () {
		sprite.enabled = true;
	}

	void Hide () {
		sprite.enabled = false;
	}
		
	public float AtackIntervalTime {
		set;
		get;
	}

	public bool IsAtacking {
		set;
		get;
	}

	public abstract IEnumerator Atack ();

	public void OnClick () {
		Debug.Log ("clicked");
		IsAtacking = true;
		GetComponent<Collider>().enabled = false;
		SoundManager.Instance.PlaySE (AudioClipID.SE_ATACK);
		SoundManager.Instance.StopBGM ();
		SoundManager.Instance.PlayBGM (AudioClipID.BGM_MAIN);
		EnemyGenerator.Instance.AttackedEnemy ();
		decimal persent = CountManager.Instance.KeepMoneyCount / 100;
		decimal damage = mEnemyData.Atack * persent;
		decimal getMoneyCount = damage  * 0.2m;
		Debug.Log ("damage = " + damage);
		Debug.Log ("getMoney = " + getMoneyCount);
		GameObject getMoneyLabelObject = Instantiate (mGetMoneyLabelPrefab) as GameObject;
		getMoneyLabelObject.transform.parent = transform.parent;
		getMoneyLabelObject.transform.localScale = new Vector3 (1, 1, 1);
		getMoneyLabelObject.transform.localPosition = transform.localPosition;
		getMoneyCount = Math.Round (getMoneyCount, 0, MidpointRounding.AwayFromZero);
		getMoneyLabelObject.SendMessage ("SetCount", "+" + CommaMarker.MarkDecimalCount(getMoneyCount));
		CountManager.Instance.AddMoneyCount (getMoneyCount);
		StatusDataKeeper.Instance.IncrementAtackEnemyCount ();

		if (destroyAnimation == null) {
			StartCoroutine (Atack());
		}else {
			destroyAnimation.enabled = true;
		}

		TweenColor.Begin (gameObject, 2.0f, Color.clear);
		//ヒストリーデータをインサート
		InsertHistoryData ();
		//宇宙海賊の場合は親の親オブジェクトにメッセージングする
		if (mEnemyData.Id == 6) {
			transform.parent.gameObject.transform.parent.gameObject.BroadcastMessage ("EnemyDestroyed");
		} else {
			transform.parent.gameObject.BroadcastMessage ("EnemyDestroyed");
		}
		StartCoroutine (DestroyCoroutine ());
	}

	private IEnumerator DestroyCoroutine () {
		yield return new WaitForSeconds (2.0f);
		//宇宙海賊の場合は親オブジェクトごとDestroyする
		if (mEnemyData.Id == 6) {
			Destroy (transform.parent.gameObject);
		} else {
			Destroy (gameObject);
		}
	}

	public void SetAtackIntervalTime () {
		AtackIntervalTime = 10.0f;
	}

	public void StartAtacking () {
		IsAtacking = true;
		float x = 0;
		float moveToY = UnityEngine.Random.Range (limitBottom, limitTop);
		Debug.Log ("moveToY = " + moveToY);
		Debug.Log ("cueentY = " + transform.localPosition.y);
		if(transform.localPosition.y < moveToY){
			//up
			mSpeedY = 1;
		}else {
			//down
			mSpeedY = -1;
		}
		if (transform.eulerAngles.y == 0) {
			x = UnityEngine.Random.Range (limitLeft, transform.localPosition.x);
		} else {
			x = UnityEngine.Random.Range (transform.localPosition.x, limitRight);
		}
		Hashtable hash = new Hashtable ();
		hash.Add ("x", x);
		hash.Add ("y", moveToY);
		hash.Add ("speed", 1000.0f);
		hash.Add ("delay", 1);
		hash.Add ("islocal", true);
		hash.Add ("oncomplete", "OnMoveAnimationCompleted");
		hash.Add ("easetype", iTween.EaseType.linear);
		iTween.MoveTo (gameObject, hash);

	}

	private void OnMoveAnimationCompleted () {
		StartCoroutine (Atack ());
	}

	public void ApplyDamage () {
		//攻撃前にタッチされていたら中止
		if (!GetComponent<Collider>().enabled) {
			return;
		}
		SoundManager.Instance.PlaySE (AudioClipID.SE_DAMAGED);
		decimal persent = CountManager.Instance.KeepMoneyCount / 100;
		decimal damage = mEnemyData.Atack * persent;
		mTotalDamage += damage;
		StatusDataKeeper.Instance.AddDamagedCount (damage);
		CountManager.Instance.DecreaseMoneyCount (damage);
		if (sprite.enabled) {
			GameObject damageLabelObject = Instantiate (mDamageLabelPrefab)as GameObject;
			damageLabelObject.transform.parent = transform.parent;
			damageLabelObject.transform.localScale = new Vector3 (1, 1, 1);
			damageLabelObject.transform.localPosition = transform.localPosition;
			damage = Math.Round (damage, 0, MidpointRounding.AwayFromZero);
			damageLabelObject.SendMessage ("SetCount", "-" + CommaMarker.MarkDecimalCount(damage));
		}
	}

	private void InsertHistoryData () {
		DateTime dtNow = DateTime.Now;
		string date = dtNow.ToString ("MM/dd HH:mm");
		mTotalDamage = Math.Round (mTotalDamage, 0, MidpointRounding.AwayFromZero);
		string damage = mTotalDamage.ToString ();
		HistoryData historyData = new HistoryData ();
		historyData.EnemyId = mEnemyData.Id;
		historyData.Damage = damage;
		historyData.Date = date;
		historyData.FlagSecom = 0;
		HistoryDataDao.Instance.InsertHistoryData (historyData);
	}
}
