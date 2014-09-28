using UnityEngine;
using System.Collections;

public class OpeningController : MonoBehaviour {
	public TweenAlpha fadeOutAnimation;

	void Start(){
		SoundManager.Instance.PlayBGM (AudioClipID.BGM_OPENING);
	}

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
		SoundManager.Instance.StopBGM ();
		Application.LoadLevel ("Tutorial");
	}
}
