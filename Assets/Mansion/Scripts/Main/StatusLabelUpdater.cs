using UnityEngine;
using System.Collections;
using System.Text;

public class StatusLabelUpdater : MonoBehaviour {

	private const string UNIT = "\u5339";
	public UILabel statusParamLabel;
	public UILabel[] statusParamlabelArray;
	
	// Update is called once per frame
	void Update () {
		StatusData statusData = StatusDataKeeper.Instance.StatusData;
		statusParamlabelArray [0].text = statusData.TotalGenerateCount + "円";
		statusParamlabelArray [1].text = statusData.MaxKeepCount + "円";
		statusParamlabelArray [2].text = statusData.FirstGenerateDate;
		statusParamlabelArray [3].text = statusData.TotalPitGenerateCount + "円";
		statusParamlabelArray [4].text = statusData.TotalTapPitCount + "回";
		statusParamlabelArray [5].text = statusData.TotalCameEnemyCount + "回";
		statusParamlabelArray [6].text = statusData.TotalAtackEnemyCount + "回";
		statusParamlabelArray [7].text = statusData.TotalUsedSecomCount + "回";
		statusParamlabelArray [8].text = statusData.TotalDamegedCount + "円";
	}
}
