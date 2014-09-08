using UnityEngine;
using System.Collections;

public class MainController : MonoBehaviour {

	public GameObject homePanel;
	public GameObject shopPanel;
	public GameObject statusPanel;
	public GameObject[] colorFilterArray;
	public UIScrollView scrollView;
	private GameObject mCurrentPanel;
	public HomePanelController homePanelController;

	void Start () {
		SoundManager.Instance.PlayBGM (AudioClipID.BGM_MAIN);
		shopPanel.SetActive (false);
		statusPanel.SetActive (false);
		scrollView.ResetPosition ();
		mCurrentPanel = homePanel;
		homePanelController.Init ();
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
		if(CheckSamePanel(homePanel)){
			return;
		}
		mCurrentPanel.SetActive (false);
		mCurrentPanel = homePanel;
		ChangeButtonFilter (0);
		homePanelController.Init ();
	}

	public void OnShopButtonClicked () {
		Debug.Log ("shop");
		SoundManager.Instance.PlaySE (AudioClipID.SE_BUTTON);
		scrollView.ResetPosition ();
		if(CheckSamePanel(shopPanel)){
			return;
		}
		if(mCurrentPanel.Equals(homePanel)){
			homePanelController.HideRoomObjects ();
		}else {
			statusPanel.SetActive (false);
		}
		mCurrentPanel = shopPanel;
		shopPanel.SetActive (true);
		ChangeButtonFilter (1);
	}

	public void OnStatusButtonClicked () {
		Debug.Log ("status");
		SoundManager.Instance.PlaySE (AudioClipID.SE_BUTTON);
		scrollView.ResetPosition ();
		if(CheckSamePanel(statusPanel)){
			return;
		}
		if(mCurrentPanel.Equals(homePanel)){
			homePanelController.HideRoomObjects ();
		}else {
			shopPanel.SetActive (false);
		}
		mCurrentPanel = statusPanel;
		statusPanel.SetActive (true);
		ChangeButtonFilter (2);
	}

	public void OnRecommendButtonClicked(){
		Debug.Log ("recommend");
		SoundManager.Instance.PlaySE (AudioClipID.SE_BUTTON);
	}

	public bool CheckCurrentIsHomePanel(){
		return mCurrentPanel.Equals (homePanel);
	}

	private bool CheckSamePanel(GameObject panelObject){
		if(mCurrentPanel.Equals(panelObject)){
			Debug.Log ("same");
			return true;
		}
		return false;
	}

	private void ChangeButtonFilter(int buttonIndex){
		for(int i = 0;i < colorFilterArray.Length;i++){
			GameObject colorFilter = colorFilterArray[i];
			if(i == buttonIndex){
				colorFilter.SetActive (false);
				continue;
			}
			colorFilter.SetActive (true);
		}
	}
}
