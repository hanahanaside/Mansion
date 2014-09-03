using UnityEngine;
using System.Collections;

public class OpeningController : MonoBehaviour {

	public TweenColor fadeOutAnimation;

	void OnLabelAnimationFinished(){
		fadeOutAnimation.PlayForward ();
	}

	public void FadeOutAnimationFinished(){
		Transition ();
	}

	public void OnSkipButtonClicked(){
		SoundManager.Instance.PlaySE (AudioClipID.SE_BUTTON);
		fadeOutAnimation.PlayForward ();
	}

	private void Transition(){
		Application.LoadLevel ("Main");
	}
}
