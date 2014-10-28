using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class WWWClient {
	public delegate void RequestFinishedDelegate (WWW response);

	public delegate void TimeOutDelegate ();

	private const float TIME_OUT = 10.0f;
	private RequestFinishedDelegate mOnSuccess;
	private RequestFinishedDelegate mOnFail;
	private TimeOutDelegate mOnTimeOut;
	private MonoBehaviour mMonoBehaviour;
	private WWW mWWW;
	private string mURL;
	private bool mIsTimeOut;

	public WWWClient (MonoBehaviour monoBehaviour, string url)
	{
		mMonoBehaviour = monoBehaviour;
		mURL = url;
	}

	public RequestFinishedDelegate OnSuccess {
		set{ mOnSuccess = value; }
	}

	public RequestFinishedDelegate OnFail {
		set{ mOnFail = value; }
	}

	public TimeOutDelegate OnTimeOut {
		set{ mOnTimeOut = value; }
	}

	public void Request () {
		mMonoBehaviour.StartCoroutine (RequestCoroutine ());
	}

	private IEnumerator RequestCoroutine () {
		mWWW = new WWW (mURL);
		yield return mMonoBehaviour.StartCoroutine (CheckTimeout ());

		if (mIsTimeOut) {
			Debug.Log ("TimeOut");
			TimeOut ();
		} else if (mWWW.error == null) {
			Debug.Log ("www ok");
			Debug.Log ("result = " + mWWW.text);
			Success ();
		} else {
			Debug.Log ("www error");
			Fail ();
		}
		mWWW.Dispose ();
	}

	private void TimeOut () {
		if (mOnTimeOut != null) {
			mOnTimeOut ();
		}
	}

	private void Success () {
		if (mOnSuccess != null) {
			mOnSuccess (mWWW);
		}
	}

	private void Fail () {
		if (mOnFail != null) {
			mOnFail (mWWW);
		}
	}

	private  IEnumerator CheckTimeout () {
		float startRequestTime = Time.time;
		while (!mWWW.isDone) {
			if (Time.time - startRequestTime < TIME_OUT)
				yield return null;
			else {
				//タイムアウト
				mIsTimeOut = true;
				break;
			}
		}
		yield return null;
	}
}
