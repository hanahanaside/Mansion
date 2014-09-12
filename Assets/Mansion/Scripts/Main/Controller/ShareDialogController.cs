using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

public class ShareDialogController : DialogController {

	public void OnShareButtonClicked () {
		SoundManager.Instance.PlaySE (AudioClipID.SE_BUTTON);
		ShareManager.Instance.Share ();
	}

	public override void OnCloseButonClicked () {
		base.OnCloseButonClicked ();
		Destroy (transform.parent.gameObject);
	}
}
