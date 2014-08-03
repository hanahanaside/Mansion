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
		mCurrentPanel = homePanel;
	}

	public void OnHomeButtonClicked () {
		Debug.Log ("home");
		scrollView.ResetPosition();
		mCurrentPanel.SetActive(false);
		homePanel.SetActive(true);
		mCurrentPanel = homePanel;
	}

	public void OnShopButtonClicked () {
		Debug.Log ("shop");
		scrollView.ResetPosition();
		mCurrentPanel.SetActive(false);
		shopPanel.SetActive(true);
		mCurrentPanel = shopPanel;
	}

	public void OnStatusButtonClicked () {
		Debug.Log ("status");
		scrollView.ResetPosition();
	}

}
