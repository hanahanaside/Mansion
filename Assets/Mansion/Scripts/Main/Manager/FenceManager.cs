using UnityEngine;
using System.Collections;

public class FenceManager : MonoBehaviour {

	public GameObject fence;
	private static FenceManager sInstance;

	// Use this for initialization
	void Start () {
		sInstance = this;
		HideFence ();
	}

	public static FenceManager Instance {
		get {
			return sInstance;
		}
	}

	public void ShowFence () {
		fence.SetActive (true);
	}

	public void HideFence () {
		fence.SetActive (false);
	}
}
