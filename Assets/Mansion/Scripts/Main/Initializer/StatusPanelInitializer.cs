using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StatusPanelInitializer : MonoBehaviour {
	public GameObject work;
	public UIGrid gotItemGrid;
	public UIGrid historyGrid;
	public GameObject historyButtonPrefab;

	void OnEnable () {
		InitHistoryGrid ();
		InitWork ();
	}

	private void InitHistoryGrid () {
		List<HistoryData> historyDataList = HistoryDataDao.Instance.GetHistoryDataList ();
		historyDataList.Reverse ();
		List<Transform> historyChildList = historyGrid.GetChildList ();
		Debug.Log ("count = " + historyChildList.Count);
		for (int i = 0; i < 4; i++) {
			//履歴が４つに満たない場合は終了
			if (i >= historyDataList.Count) {
				Debug.Log ("break");
				break;
			}

			GameObject historyChild = null;
			if (i >= historyChildList.Count) {
				historyChild = Instantiate (historyButtonPrefab) as GameObject;
				historyGrid.AddChild (historyChild.transform);
				historyChild.transform.localScale = new Vector3 (1, 1, 1);
			} else {
				historyChild = historyChildList [i].gameObject;
			}
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
