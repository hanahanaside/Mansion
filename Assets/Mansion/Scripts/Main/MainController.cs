using UnityEngine;
using System.Collections;

public class MainController : MonoBehaviour {

	public GameObject homePanel;
	public GameObject shopPanel;
	public GameObject statusPanel;
	public UIScrollView scrollView;
	private GameObject mCurrentPanel;

	void Start () {
		shopPanel.SetActive (false);
		statusPanel.SetActive (false);
		mCurrentPanel = homePanel;
		SecomData secomData = new SecomData();
		secomData.Count = 1;
		secomData.MacxCount = 2;
	}

	public void OnHomeButtonClicked () {
		Debug.Log ("home");
		ChangePanel (homePanel);
	}

	public void OnShopButtonClicked () {
		Debug.Log ("shop");
		ChangePanel (shopPanel);
	}

	public void OnStatusButtonClicked () {
		Debug.Log ("status");
		ChangePanel (statusPanel);
	}

	private void ChangePanel (GameObject panel) {
		if(mCurrentPanel.Equals(panel)){
			Debug.Log("same");
			scrollView.ResetPosition ();
			return;
		}
		panel.SetActive (true);
		mCurrentPanel.SetActive (false);
		mCurrentPanel = panel;
		scrollView.ResetPosition ();
	}
}
