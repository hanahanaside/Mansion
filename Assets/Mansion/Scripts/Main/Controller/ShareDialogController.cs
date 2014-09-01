using UnityEngine;
using System.Collections;

public class ShareDialogController : DialogController {

	public void OnShareButtonClicked(){
		SoundManager.Instance.PlaySE (AudioClipID.SE_BUTTON);
	}

	public override void OnCloseButonClicked () {
		base.OnCloseButonClicked ();
		Destroy (transform.parent.gameObject);
	}

}
