using UnityEngine;
using System.Collections;

public class TutorialController : MonoBehaviour {

	void Start () {
		#if UNITY_EDITOR
		//	Application.LoadLevel ("Main");
		#endif
		SoundManager.Instance.PlayBGM (AudioClipID.BGM_MAIN);
	}
}
