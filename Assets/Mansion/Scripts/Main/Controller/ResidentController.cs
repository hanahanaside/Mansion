﻿using UnityEngine;
using System.Collections;

public class ResidentController : HumanController {
	void SetDepth (int roomId) {
		switch (roomId) {
		case 1:
			sprite.depth = 101;
			break;
		case 2:
			sprite.depth = 91;
			break;
		case 3:
			sprite.depth = 81;
			break;
		case 4:
			sprite.depth = 71;
			break;
		case 5:
			sprite.depth = 61;
			break;
		case 6:
			sprite.depth = 51;
			break;
		case 7:
			sprite.depth = 41;
			break;
		case 8:
			sprite.depth = 31;
			break;
		case 9:
			sprite.depth = 21;
			break;
		case 10:
			sprite.depth = 11;
			break;
		case 11:
			sprite.depth = 1;
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
