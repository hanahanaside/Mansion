using UnityEngine;
using System.Collections;

public class HistoryButtonController : MonoBehaviour {

	public UILabel historyLabel;
	public UISprite historySprite;

	void Init (HistoryData historydata) {
		historyLabel.text = historydata.Date +"\n" + historydata.Result;
	}
}
