using UnityEngine;
using System.Collections;

public class ResidentController : HumanController {

	void Update () {
		Walk();
	}

	void Init(){
		sprite.enabled = true;
	}

	void Hide(){
		sprite.enabled = false;
	}
}
