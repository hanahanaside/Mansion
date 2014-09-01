using UnityEngine;
using System.Collections;

public class HistoryImageController : MonoBehaviour {

	public UISprite securitySprite;

	void ShowSecuritySprite(){
		securitySprite.enabled = true;
	}

	void HideSecuritySprite(){
		securitySprite.enabled = false;
	}
}
