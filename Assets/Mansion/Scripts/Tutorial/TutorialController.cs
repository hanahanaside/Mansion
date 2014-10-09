using UnityEngine;
using System.Collections;

public class TutorialController : MonoBehaviour {
	public TutorialWebView tutorialWebView;
	private float mInterval = 5.0f;
	private bool mPaused = false;

	void Start () {
		#if UNITY_EDITOR
		Application.LoadLevel ("Main");
		#endif
		SoundManager.Instance.PlayBGM (AudioClipID.BGM_MAIN);
	}

	void Update () {
		if (mPaused) {
			return;
		}
		mInterval -= Time.deltaTime;
		if (mInterval < 0.0f) {
			mPaused = true;
			//tutorialWebView.Hide ();
			//ShowTutorialBonusDialog ();
		}
	}

	private void ShowTutorialBonusDialog () {
		string title = "チュートリアル完了ボーナス！";
		string message = "お金を3000円プレゼント！";
		AlertDialog alertDialog = new AlertDialog ();
		alertDialog.OnPositiveButtonClicked = () => {
			PrefsManager.Instance.FlagTutorialFinished = 1;
			HistoryData historyData = new HistoryData ();
			historyData.Damage = "31";
			historyData.EnemyId = 2;
			historyData.FlagSecom = 0;
			historyData.Date = System.DateTime.Now.ToString ("MM/dd HH:mm");
			HistoryDataDao.Instance.InsertHistoryData (historyData);
			Application.LoadLevel ("Main");
		};
		alertDialog.Show (title, message, "OK");
	}

	public void OnSkipButtonClicked () {
		ShowTutorialBonusDialog ();
	}
}
