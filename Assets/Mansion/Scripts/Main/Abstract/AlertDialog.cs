using UnityEngine;
using System.Collections;

public class AlertDialog {
	public delegate void PositiveButtonClickedDelegate ();

	public delegate void NegativeButtonClickedDelegate ();

	private PositiveButtonClickedDelegate mOnPositiveButtonClicked;
	private NegativeButtonClickedDelegate mOnNegativeButtonClicked;
	private string mTitle;
	private string mMessage;
	private string mPositiveButton;
	private string mNegativeButton;

	public AlertDialog ()
	{
		#if UNITY_IPHONE
		EtceteraManager.alertButtonClickedEvent += alertButtonClickedEvent;
		#endif

		#if UNITY_ANDROID
		EtceteraAndroidManager.alertButtonClickedEvent += alertButtonClickedEvent;
		EtceteraAndroidManager.alertCancelledEvent += alertCancelledEvent;
		#endif
	}

	void alertButtonClickedEvent (string clickedButton) {
		#if UNITY_IPHONE
		EtceteraManager.alertButtonClickedEvent -= alertButtonClickedEvent;
		#endif

		#if UNITY_ANDROID
		EtceteraAndroidManager.alertButtonClickedEvent -= alertButtonClickedEvent;
		EtceteraAndroidManager.alertCancelledEvent -= alertCancelledEvent;
		#endif
		if (clickedButton == mPositiveButton) {
			mOnPositiveButtonClicked ();
		}

		if (clickedButton == mNegativeButton) {
			mOnNegativeButtonClicked ();
		}
	}

	void alertCancelledEvent () {
		if (string.IsNullOrEmpty (mNegativeButton)) {
			Show (mTitle, mMessage, mPositiveButton);
		} else {
			Show (mTitle, mMessage, mPositiveButton, mNegativeButton);
		}
	}

	public PositiveButtonClickedDelegate OnPositiveButtonClicked {
		set {
			mOnPositiveButtonClicked = value;
		}
	}

	public NegativeButtonClickedDelegate OnNegativeButtonClicked {
		set {
			mOnNegativeButtonClicked = value;
		}
	}

	public void Show (string title, string message, string positiveButton) {
		mTitle = title;
		mMessage = message;
		mPositiveButton = positiveButton;
		#if UNITY_IPHONE
		string[] buttons = { positiveButton };
		EtceteraBinding.showAlertWithTitleMessageAndButtons (title, message, buttons);
		#endif
		#if UNITY_ANDROID
		EtceteraAndroid.showAlert(title,message,positiveButton);
		#endif
	}

	public void Show (string title, string message, string positiveButton, string negativeButton) {
		mTitle = title;
		mMessage = message;
		mPositiveButton = positiveButton;
		mNegativeButton = negativeButton;
		#if UNITY_IPHONE
		string[] buttons = { positiveButton, negativeButton };
		EtceteraBinding.showAlertWithTitleMessageAndButtons (title, message, buttons);
		#endif
		#if UNITY_ANDROID
		EtceteraAndroid.showAlert(title,message,positiveButton,negativeButton);
		#endif
	}
}
