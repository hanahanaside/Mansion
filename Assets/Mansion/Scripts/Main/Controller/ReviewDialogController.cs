using UnityEngine;
using System.Collections;

public class ReviewDialogController : DialogController {

	public void OnReviewButtonClicked(){
		SoundManager.Instance.PlaySE (AudioClipID.SE_BUTTON);
		string url = "";
		#if UNITY_IPHONE
		url = "https://itunes.apple.com/jp/app/id921934428?mt=8/";
		#endif

		#if UNITY_ANDROID
		url = "https://play.google.com/store/apps/details?id=com.shakky.money";
		#endif
		if(PrefsManager.Instance.FlagReview == 0){
			CountManager.Instance.AddMoneyCount (50000L);
			PrefsManager.Instance.SaveReviewed ();
		}
		Application.OpenURL (url);
	}

	public override void OnCloseButonClicked () {
		base.OnCloseButonClicked ();
		Destroy (transform.parent.gameObject);
	}

}
