using UnityEngine;
using System.Collections;

public class ResidentController : HumanController {

	void SetDepth (int roomId) {
		switch (roomId) {
		case 1:
			sprite.depth = 1001;
			break;
		case 2:
			sprite.depth = 901;
			break;
		case 3:
			sprite.depth = 801;
			break;
		case 4:
			sprite.depth = 701;
			break;
		case 5:
			sprite.depth = 601;
			break;
		case 6:
			sprite.depth = 501;
			break;
		case 7:
			sprite.depth = 401;
			break;
		case 8:
			sprite.depth = 301;
			break;
		case 9:
			sprite.depth = 201;
			break;
		case 10:
			sprite.depth = 101;
			break;
		case 11:
			sprite.depth = 10;
			break;
		}
	}

	void Update () {
		Walk ();
	}

	void Init () {
		sprite.enabled = true;
	}

	void Hide () {
		sprite.enabled = false;
	}


}
