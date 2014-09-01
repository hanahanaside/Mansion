using UnityEngine;
using System.Collections;

public class MainController : MonoBehaviour {

	public GameObject homeGrid;
	public GameObject shopPanel;
	public GameObject statusPanel;
	public UIScrollView scrollView;
	private GameObject mCurrentPanel;
	public HomePanelInitializer homePanelInitializer;

	void Start () {
		SoundManager.Instance.PlayBGM (AudioClipID.BGM_MAIN);
		shopPanel.SetActive (false);
		statusPanel.SetActive (false);
		mCurrentPanel = homeGrid;
		SecomData secomData = new SecomData ();
		secomData.Count = 1;
		secomData.MacxCount = 2;
		scrollView.ResetPosition ();
		mCurrentPanel = homeGrid;
		homePanelInitializer.Init ();
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
		ChangePanel (homeGrid);
		homePanelInitializer.Init ();
	}

	public void OnShopButtonClicked () {
		Debug.Log ("shop");
		SoundManager.Instance.PlaySE (AudioClipID.SE_BUTTON);
		ChangePanel (shopPanel);
		scrollView.ResetPosition ();
	}

	public void OnStatusButtonClicked () {
		Debug.Log ("status");
		SoundManager.Instance.PlaySE (AudioClipID.SE_BUTTON);
		ChangePanel (statusPanel);
		scrollView.ResetPosition ();
	}

	public void OnRecommendButtonClicked(){
		Debug.Log ("recommend");
		SoundManager.Instance.PlaySE (AudioClipID.SE_BUTTON);
	}

	private void ChangePanel(GameObject panel){
		mCurrentPanel.SetActive (false);
		panel.SetActive (true);
		mCurrentPanel = panel;
	}
}
