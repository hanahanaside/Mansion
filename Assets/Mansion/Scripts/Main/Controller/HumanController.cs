using UnityEngine;
using System.Collections;

public abstract class HumanController : MonoBehaviour {
	public UISpriteAnimation walkAnimation;
	public UISprite sprite;
	public float limitLeft;
	public float limitRight;
	public float limitTop;
	public float limitBottom;
	private float mSpeedX;
	private float mSpeedY;
	private float mTime;

	void MoveDepth(int roomDepth){
		if(mSpeedY < 0f ){
			sprite.depth = roomDepth + 1;
		}else if(mSpeedY > 0f){
			sprite.depth = roomDepth - 1;
		}
	}

	public void Walk () {
		mTime -= Time.deltaTime;
		if (mTime < 0) {
			ChangeMove ();
		}
		if (mSpeedX == 0 && mSpeedY == 0) {
			return;
		}
		float x = transform.localPosition.x;
		float y = transform.localPosition.y;
		if (x < limitLeft) {
			TurnRight ();
		}
		if (x > limitRight) {
			TurnLeft ();
		}
		if (y > limitTop) {
			transform.Translate (0, -0.001f, 0);
			mSpeedY = -mSpeedY;
		}
		if (y < limitBottom) {
			transform.Translate (0, 0.001f, 0);
			mSpeedY = -mSpeedY;
		}
		transform.Translate (mSpeedX, mSpeedY, 0);
	}

	public void StopWalkAnimation () {
		if (walkAnimation != null) {
			walkAnimation.enabled = false;
		}
	}

	public void RestartWalkAnimation () {
		SetSpeed ();
		if (walkAnimation != null) {
			walkAnimation.enabled = true;
		}
	}

	private void ChangeMove () {
		if (mSpeedX != 0) {
			mSpeedX = 0;
			mSpeedY = 0;
			StopWalkAnimation ();
			SetTime ();
		} else {
			RestartWalkAnimation ();
			SetSpeed ();
			SetTime ();
		}
	}

	private void SetTime () {
		mTime = Random.Range (0.1f, 4.0f);
	}

	private void SetSpeed () {
		mSpeedX = Random.Range (-0.002f, 0);
		mSpeedY = Random.Range (-0.002f, 0.002f);
		int turn = Random.Range (0, 2);
		switch (turn) {
		case 0:
			TurnLeft ();
			break;
		case 1:
			TurnRight ();
			break;
		}
	}

	private void TurnLeft () {
		transform.eulerAngles = new Vector3 (0, 0, 0);
	}

	private void TurnRight () {
		transform.eulerAngles = new Vector3 (0, 180, 0);
	}
}
