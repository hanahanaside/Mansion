using UnityEngine;
using System.Collections;

public class UnLockAnimator : MonoBehaviour {
	public GameObject IconObject;
	public GameObject IconParentObject;
	public GameObject keepOutObject;
	public GameObject fenceObject;

	public void PlayAnimation () {
		SoundManager.Instance.PlaySE (AudioClipID.SE_UNLOCK_ROOM);
		PlayRotateLockAnimation ();
	}
		
	void OnCompleteRotateLockAnimation () {
		PlayMoveLockAnimation ();
	}

	void OnCompleteMoveLockAnimation () {
		Destroy (IconParentObject);
		PlayMoveKeepOutAnimation ();
	}

	void OnCompleteMoveKeepOutAnimation () {
		Destroy (keepOutObject);
		TweenColor.Begin (fenceObject, 2, Color.clear);
	}

	private void PlayMoveKeepOutAnimation () {
		Hashtable table = new Hashtable ();
		table.Add ("y", 3);
		table.Add ("easeType", "easeInBack");
		table.Add ("time", 0.5);
		table.Add ("oncompletetarget", this.gameObject);
		table.Add ("oncomplete", "OnCompleteMoveKeepOutAnimation");
		iTween.MoveAdd (keepOutObject, table);
	}

	private void PlayRotateLockAnimation () {
		Hashtable table = new Hashtable ();
		table.Add ("z", -30);
		table.Add ("easeType", "linear");
		table.Add ("time", 0.5);
		table.Add ("oncompletetarget", this.gameObject);
		table.Add ("oncomplete", "OnCompleteRotateLockAnimation");
		iTween.RotateAdd (IconObject, table);
	}

	private void PlayMoveLockAnimation () {
		Hashtable table = new Hashtable ();
		table.Add ("y", -1);
		table.Add ("easeType", "linear");
		table.Add ("time", 0.5);
		table.Add ("oncompletetarget", this.gameObject);
		table.Add ("oncomplete", "OnCompleteMoveLockAnimation");
		iTween.MoveAdd (IconParentObject, table);
	}
}
