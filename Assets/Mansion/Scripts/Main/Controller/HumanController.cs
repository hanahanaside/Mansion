using UnityEngine;
using System.Collections;

public abstract class HumanController : MonoBehaviour {

	public UISpriteAnimation spriteAnimation;
	private float mSpeedX;
	private float mSpeedY;
	private float mTime;
	
	public void Walk(){
		mTime -= Time.deltaTime;
		if (mTime < 0) {
			ChangeMove ();
		}
		if (mSpeedX == 0 && mSpeedY == 0) {
			return;
		}
		float x = transform.localPosition.x;
		float y = transform.localPosition.y;
		if (x < -280) {
			TurnRight();
		}
		if (x > 280) {
			TurnLeft();
		}
		if (y > -80) {
			mSpeedY = -mSpeedY;
		}
		if (y < -240) {
			mSpeedY = -mSpeedY;
		}
		transform.Translate (mSpeedX, mSpeedY, 0);
	}

	private void ChangeMove () {
		if (mSpeedX != 0) {
			mSpeedX = 0;
			mSpeedY = 0;
			spriteAnimation.enabled = false;
			SetTime ();
		} else {
			spriteAnimation.enabled = true;
			SetSpeed ();
			SetTime ();
		}
	}

	private void SetTime () {
		mTime = Random.Range (0.1f, 4.0f);
	}

	private void SetSpeed () {
		mSpeedX = Random.Range (-0.002f,0);
		mSpeedY = Random.Range (-0.002f,0);
		int turn = Random.Range (0, 2);
		switch (turn) {
		case 0:
			TurnLeft();
			break;
		case 1:
			TurnRight();
			break;
		}
	}

	private void TurnLeft(){
		transform.eulerAngles = new Vector3 (0, 0, 0);
	}

	private void TurnRight(){
		transform.eulerAngles = new Vector3 (0, 180, 0);
	}
}
