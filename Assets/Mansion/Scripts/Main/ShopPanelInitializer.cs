using UnityEngine;
using System.Collections;

public class ShopPanelInitializer : MonoBehaviour {

	public GameObject shopItemButtonPrefab;
	public UIGrid grid;

	void OnEnable () {

	}

	// Use this for initialization
	void Start () {
		for (int i = 0; i<44; i++) {
			GameObject shopButton = Instantiate (shopItemButtonPrefab) as GameObject;
			shopButton.name = "shopButton" + i;
			grid.AddChild (shopButton.transform);
			shopButton.transform.localScale = new Vector3 (1, 1, 1);
		}
	}
}
