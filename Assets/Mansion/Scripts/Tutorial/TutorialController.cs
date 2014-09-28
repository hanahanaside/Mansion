using UnityEngine;
using System.Collections;

public class TutorialController : MonoBehaviour {
	public TutorialWebView tutorialWebView;
	private float mInterval = 5.0f;
	private bool mPaused = false;

	void Start(){
		#if UNITY_EDITOR
		Application.LoadLevel ("Main");
		#endif
	}

	void Update () {
		if (mPaused) {
			return;
		}
		mInterval -= Time.deltaTime;
		if (mInterval < 0.0f) {
			mPaused = true;
			tutorialWebView.Hide ();
			ShowTutorialBonusDialog ();
		}
	}

	private void ShowTutorialBonusDialog () {
		string title = "チュートリアル完了ボーナス！";
		string message = "お金を3000円プレゼント！";
		AlertDialog alertDialog = new AlertDialog ();
		alertDialog.OnPositiveButtonClicked = () => {
			PrefsManager.Instance.FlagTutorialFinished = 1;
			Application.LoadLevel ("Main");
		};
		alertDialog.Show (title, message, "OK");
	}
}
