using UnityEngine;
using System.Collections;
using System.Text;

public class ShareBoostTimeKeeper : MonoBehaviour {
	public UILabel shareLabel;
	private static ShareBoostTimeKeeper sInstance;
	private float mBoostTime;

	void Start () {
		sInstance = this;
		mBoostTime = PrefsManager.Instance.BoostTime;
	}

	void Update () {
		if (mBoostTime <= 0) {
			return;
		}
		mBoostTime -= Time.deltaTime;
		int min = (int)(mBoostTime / 60);
		int sec = (int)(mBoostTime % 60);
		StringBuilder sb = new StringBuilder ();
		sb.Append ("ブースト中!!\n");
		sb.Append ("終了まで" + min + "分" + sec + "秒");
		shareLabel.text = sb.ToString ();
		shareLabel.color = new Color (1f, 0.7f, 0.016f, 1f);
		if(mBoostTime <= 0){
			shareLabel.text = "シェアしてGET！";
			shareLabel.color = Color.black;
			CountManager.Instance.StopBoost ();
		}
	}

	void OnApplicationPause (bool pauseStatus) {
		if (pauseStatus) {
			PrefsManager.Instance.BoostTime = mBoostTime;
		}
	}

	public void StartBoost () {
		mBoostTime = 10.0f;
	}

	public static ShareBoostTimeKeeper Instance {
		get {
			return sInstance;
		}
	}
}
