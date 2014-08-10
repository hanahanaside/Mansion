using UnityEngine;
using System.Collections;

public class UIRootInstanceKeeper : MonoBehaviour {

	private static GameObject sUiRoot;

	// Use this for initialization
	void Start () {
		sUiRoot = gameObject;
	}
	
	public static GameObject UIRootGameObject {
		get {
			return sUiRoot;
		}
	}
}
