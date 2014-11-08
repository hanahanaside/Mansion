using UnityEngine;
using System.Collections;
using MiniJSON;
using System.Collections.Generic;

public class MainController : MonoBehaviour {
	public GameObject homePanel;
	public GameObject shopPanel;
	public GameObject statusPanel;
	public GameObject rectanglePanel;
	public GameObject[] colorFilterArray;
	public GameObject cpiButton;
	public UIScrollView scrollView;
	public HomePanelController homePanelController;
	public UISprite exSprite;
	public UISprite rectangleBackground;
	public UITexture recommendTexture;
	private GameObject mCurrentPanel;
	private int mSwitchStatusCount;

	void Awake(){
		#if UNITY_IPHONE
		WWWClient wwwClient = new WWWClient (this,"http://ad.graasb.com/shakky/money/json/environment.json");
		wwwClient.OnSuccess = (WWW response) => {
			//JSONテキストのデコード
			Dictionary<string,object> jsonData = MiniJSON.Json.Deserialize(response.text) as Dictionary<string,object>;
			string environment = (string)jsonData["environment"];
			if(environment == "production"){
				cpiButton.SetActive(true);
			}
		};
		wwwClient.Request ();
		#endif
	}

	void Start () {
		SoundManager.Instance.PlayBGM (AudioClipID.BGM_MAIN);
		shopPanel.SetActive (false);
		statusPanel.SetActive (false);
		scrollView.ResetPosition ();
		mCurrentPanel = homePanel;
		homePanelController.Init ();
		if (Application.internetReachability != NetworkReachability.NotReachable) {
			LoadRecommendTexture ();
			BannerAd.Instance.Show ();
			IconAd.Instance.ShowIconAd ();
		}
	}

	void Update () {
		#if !UNITY_EDITOR
		float y = scrollView.transform.localPosition.y;
		if (y < -15f) {
			IconAd.Instance.SetDownMargins ();
		}else {
			IconAd.Instance.SetDefaultMargins ();
		}
		#endif

#if UNITY_ANDROID
		if (Input.GetKey (KeyCode.Escape)) {
			NotificationManager.Instance.ScheduleLocalNotification ();
			Application.Quit ();
			Debug.Log ("finish");
			return;
		}
#endif
	}

	void OnApplicationPause (bool pauseStatus) {
		if (!pauseStatus) {
			PopAdManager.Instance.ShowPopAd ();
		}
	}

	public void OnHomeButtonClicked () {
		Debug.Log ("home");
		SoundManager.Instance.PlaySE (AudioClipID.SE_BUTTON);
		if (CheckSamePanel (homePanel)) {
			return;
		}
		if (Application.internetReachability != NetworkReachability.NotReachable) {
			IconAd.Instance.ShowIconAd ();
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
		if (CheckSamePanel (shopPanel)) {
			return;
		}
		if (exSprite.enabled) {
			exSprite.enabled = false;
		}
		DestroyMoneyEffect ();
		StartCoroutine (ShowInterstitialCoroutine ());
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
		DestroyMoneyEffect ();
		mSwitchStatusCount++;
		if (mSwitchStatusCount % 5 == 0) {
			StartCoroutine (ShowRectangleCoroutine ());
		}
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

	private IEnumerator ShowRectangleCoroutine () {
		yield return new WaitForSeconds (0.5f);
		// check network
		if (Application.internetReachability == NetworkReachability.NotReachable) {
			return true;
		}
		BannerAd.Instance.Hide ();
		RectangleAd.Instance.Show ();
		rectanglePanel.SetActive (true);
		#if UNITY_IPHONE
		int height = Screen.height;
		if (height == 960) {
			rectangleBackground.enabled = false;
		}
		#endif
		FenceManager.Instance.ShowFence ();
		Debug.Log ("showRectangle");
	}

	private IEnumerator ShowInterstitialCoroutine () {
		yield return new WaitForSeconds (0.5f);
		NendAdInterstitial.Instance.Dismiss ();
		NendAdInterstitial.Instance.Show ();
		Debug.Log ("showInterstitial");
	}

	public void OnRecommendButtonClicked () {
		Debug.Log ("recommend");
		SoundManager.Instance.PlaySE (AudioClipID.SE_BUTTON);
		#if UNITY_IPHONE
		Binding.ChkAppListView ();
		#endif

		#if UNITY_ANDROID
		using (AndroidJavaClass cls_UnityPlayer = new AndroidJavaClass ("com.unity3d.player.UnityPlayer")) {

			using (AndroidJavaObject obj_Activity = cls_UnityPlayer.GetStatic<AndroidJavaObject> ("currentActivity")) {

				obj_Activity.CallStatic ("adcrops");
			}
		}
		#endif
	}

	public void OnCloseRectangleButtonClicked () {
		Debug.Log ("close");
		SoundManager.Instance.PlaySE (AudioClipID.SE_BUTTON);
		BannerAd.Instance.Show ();
		rectanglePanel.SetActive (false);
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

	private void DestroyMoneyEffect () {
		GameObject[] moneyEffectArray = GameObject.FindGameObjectsWithTag ("MoneyEffect");
		foreach (GameObject moneyEffect in moneyEffectArray) {
			Destroy (moneyEffect);
		}
	}

	private void LoadRecommendTexture () {
		WWWClient wwwClient = new WWWClient (this, "http://ad.graasb.com/shakky/money/link/img/01.png");
		wwwClient.OnSuccess = (WWW response) => {
			recommendTexture.mainTexture = response.texture;
		};
		wwwClient.Request ();
	}
}
