using UnityEngine;
using System.Collections;

public class MainController : MonoBehaviour {
	public GameObject homePanel;
	public GameObject shopPanel;
	public GameObject statusPanel;
	public GameObject closeRetangleButton;
	public GameObject[] colorFilterArray;
	public UIScrollView scrollView;
	public HomePanelController homePanelController;
	private GameObject mCurrentPanel;

	void Start () {
		SoundManager.Instance.PlayBGM (AudioClipID.BGM_MAIN);
		BannerAd.Instance.ShowBannerAd ();
		IconAd.Instance.ShowIconAd ();
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
		if (CheckSamePanel (homePanel)) {
			return;
		}
		IconAd.Instance.ShowIconAd ();
		mCurrentPanel.SetActive (false);
		mCurrentPanel = homePanel;
		ChangeButtonFilter (0);
		homePanelController.Init ();
	}

	public void OnShopButtonClicked () {
		Debug.Log ("shop");
		SoundManager.Instance.PlaySE (AudioClipID.SE_BUTTON);
		scrollView.ResetPosition ();
		if (CheckSamePanel (shopPanel)) {
			return;
		}
		NendAdInterstitial.Instance.Show ();
		if (mCurrentPanel.Equals (homePanel)) {
			homePanelController.HideRoomObjects ();
		} else {
			statusPanel.SetActive (false);
		}
		IconAd.Instance.HideIconAd ();
		mCurrentPanel = shopPanel;
		shopPanel.SetActive (true);
		ChangeButtonFilter (1);
	}

	public void OnStatusButtonClicked () {
		Debug.Log ("status");
		SoundManager.Instance.PlaySE (AudioClipID.SE_BUTTON);
		scrollView.ResetPosition ();
		if (CheckSamePanel (statusPanel)) {
			return;
		}
		RectangleAd.Instance.Show ();
		closeRetangleButton.SetActive (true);
		FenceManager.Instance.ShowFence ();
		if (mCurrentPanel.Equals (homePanel)) {
			homePanelController.HideRoomObjects ();
		} else {
			shopPanel.SetActive (false);
		}
		IconAd.Instance.HideIconAd ();
		mCurrentPanel = statusPanel;
		statusPanel.SetActive (true);
		ChangeButtonFilter (2);
	}

	public void OnRecommendButtonClicked () {
		Debug.Log ("recommend");
		SoundManager.Instance.PlaySE (AudioClipID.SE_BUTTON);
		APUnityPlugin.ShowAppliPromotionWall ();
	}

	public void OnCloseRectangleButtonClicked () {
		Debug.Log ("close");
		SoundManager.Instance.PlaySE (AudioClipID.SE_BUTTON);
		closeRetangleButton.SetActive (false);
		RectangleAd.Instance.Hide ();
		FenceManager.Instance.HideFence ();
	}

	public bool CheckCurrentIsHomePanel () {
		return mCurrentPanel.Equals (homePanel);
	}

	private bool CheckSamePanel (GameObject panelObject) {
		if (mCurrentPanel.Equals (panelObject)) {
			Debug.Log ("same");
			return true;
		}
		return false;
	}

	private void ChangeButtonFilter (int buttonIndex) {
		for (int i = 0; i < colorFilterArray.Length; i++) {
			GameObject colorFilter = colorFilterArray [i];
			if (i == buttonIndex) {
				colorFilter.SetActive (false);
				continue;
			}
			colorFilter.SetActive (true);
		}
	}
}
