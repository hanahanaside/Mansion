using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StatusPanelInitializer : MonoBehaviour {

	public GameObject work;
	public UIGrid gotItemGrid;

	void OnEnable () {
		List<Transform> childList = gotItemGrid.GetChildList ();
		int count = childList.Count;
		Transform finalChild = childList [count - 1];
		work.transform.localPosition = new Vector3(0,finalChild.localPosition.y - 450.0f,0);
	}
}
