using UnityEngine;
using System.Collections;

public class OpeningController : MonoBehaviour {
	public TweenAlpha fadeOutAnimation;

	public  void OnLabelAnimationFinished () {
		fadeOutAnimation.PlayForward ();
	}

	public void FadeOutAnimationFinished () {
		Transition ();
	}

	public void OnSkipButtonClicked () {
		SoundManager.Instance.PlaySE (AudioClipID.SE_BUTTON);
		fadeOutAnimation.PlayForward ();
	}

	private void Transition () {
		PrefsManager.Instance.FlagOpeningFinished = 1;
		Application.LoadLevel ("Main");
	}
}
