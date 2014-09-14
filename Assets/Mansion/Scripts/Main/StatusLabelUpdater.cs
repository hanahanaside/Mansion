using UnityEngine;
using System.Collections;
using System.Text;
using System;

public class StatusLabelUpdater : MonoBehaviour {
	private const string UNIT = "\u5339";
	public UILabel statusParamLabel;
	public UILabel[] statusParamlabelArray;
	// Update is called once per frame
	void Update () {
		StatusData statusData = StatusDataKeeper.Instance.StatusData;
		statusParamlabelArray [0].text = CommaMarker.MarkDecimalCount (statusData.TotalGenerateCount) + "円";
		statusParamlabelArray [1].text = CommaMarker.MarkDecimalCount (statusData.MaxKeepCount) + "円";
		statusParamlabelArray [2].text = statusData.FirstGenerateDate;
		statusParamlabelArray [3].text = CommaMarker.MarkLongCount(statusData.TotalPitGenerateCount) + "円";
		statusParamlabelArray [4].text = CommaMarker.MarkLongCount(statusData.TotalTapPitCount) + "回";
		statusParamlabelArray [5].text = CommaMarker.MarkLongCount(statusData.TotalCameEnemyCount) + "回";
		statusParamlabelArray [6].text = CommaMarker.MarkLongCount(statusData.TotalAtackEnemyCount) + "回";
		statusParamlabelArray [7].text = CommaMarker.MarkLongCount(statusData.TotalUsedSecomCount) + "回";
		statusParamlabelArray [8].text = CommaMarker.MarkDecimalCount(statusData.TotalDamegedCount) + "円";
	}
}
