using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShopPanelInitializer : MonoBehaviour {

	public GameObject shopItemButtonPrefab;
	public GameObject spaceSprite;
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
		List<Transform> childList = grid.GetChildList();
		int count = childList.Count;
		Transform finalChild = childList[count -1];
		spaceSprite.transform.localPosition = new Vector3(0,finalChild.localPosition.y -230.0f,0);
	}
}
