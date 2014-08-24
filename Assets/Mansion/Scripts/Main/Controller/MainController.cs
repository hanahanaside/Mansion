using UnityEngine;
using System.Collections;

public class MainController : MonoBehaviour {

	public GameObject homePanel;
	public GameObject shopPanel;
	public GameObject statusPanel;
	public UIScrollView scrollView;
	private GameObject mCurrentPanel;

	void Start () {
		SoundManager.Instance.PlayBGM (AudioClipID.BGM_MAIN);
		shopPanel.SetActive (false);
		statusPanel.SetActive (false);
		mCurrentPanel = homePanel;
		SecomData secomData = new SecomData ();
		secomData.Count = 1;
		secomData.MacxCount = 2;
	}

	void Update () {
#if UNITY_ANDROID
		if (Input.GetKey (KeyCode.Escape)) {
			Application.Quit ();
			Debug.Log("finish");
			return;
		}
#endif
	}

	public void OnHomeButtonClicked () {
		Debug.Log ("home");
		SoundManager.Instance.PlaySE (AudioClipID.SE_BUTTON);
		ChangePanel (homePanel);
	}

	public void OnShopButtonClicked () {
		Debug.Log ("shop");
		SoundManager.Instance.PlaySE (AudioClipID.SE_BUTTON);
		ChangePanel (shopPanel);
	}

	public void OnStatusButtonClicked () {
		Debug.Log ("status");
		SoundManager.Instance.PlaySE (AudioClipID.SE_BUTTON);
		ChangePanel (statusPanel);
	}

	private void ChangePanel (GameObject panel) {
		if (mCurrentPanel.Equals (panel)) {
			Debug.Log ("same");
			SoundManager.Instance.PlaySE (AudioClipID.SE_BUTTON);
			scrollView.ResetPosition ();
			return;
		}
		panel.SetActive (true);
		mCurrentPanel.SetActive (false);
		mCurrentPanel = panel;
		scrollView.ResetPosition ();
	}
}
