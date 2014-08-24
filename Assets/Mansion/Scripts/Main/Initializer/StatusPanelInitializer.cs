using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StatusPanelInitializer : MonoBehaviour {

	public GameObject work;
	public UIGrid gotItemGrid;
	public UIGrid historyGrid;

	void OnEnable () {
		InitHistoryGrid ();
		InitWork ();
	}

	private void InitHistoryGrid () {
		List<HistoryData> historyDataList = HistoryDataDao.Instance.GetHistoryDataList ();
		List<Transform> historyChildList = historyGrid.GetChildList ();
		for (int i = 0; i<4; i++) {
			GameObject historyChild = historyChildList [i].gameObject;
			HistoryData historyData = historyDataList [i];
			historyChild.BroadcastMessage ("Init", historyData);
		}
	}

	private void InitWork () {
		List<Transform> childList = gotItemGrid.GetChildList ();
		int count = childList.Count;
		Transform finalChild = childList [count - 1];
		work.transform.localPosition = new Vector3 (0, finalChild.localPosition.y - 450.0f, 0);
	}
}
