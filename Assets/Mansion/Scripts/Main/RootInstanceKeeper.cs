using UnityEngine;
using System.Collections;

public class RootInstanceKeeper : MonoBehaviour {

	private static GameObject sUiRoot;

	// Use this for initialization
	void Start () {
		sUiRoot = gameObject;
	}
	
	public static GameObject Instance {
		get {
			return sUiRoot;
		}
	}
}
