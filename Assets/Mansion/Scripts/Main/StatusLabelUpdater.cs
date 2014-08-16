using UnityEngine;
using System.Collections;
using System.Text;

public class StatusLabelUpdater : MonoBehaviour {

	private const string UNIT = "\u5339";
	public UILabel statusParamLabel;
	
	// Update is called once per frame
	void Update () {
		StatusData statusData = StatusDataKeeper.Instance.StatusData;
		StringBuilder sb = new StringBuilder ();
		sb.Append (statusData.TotalGenerateCount + UNIT + "\n");
		sb.Append (statusData.MaxKeepCount + UNIT + "\n");
		sb.Append (statusData.FirstGenerateDate + "\n");
		sb.Append (statusData.TotalPitGenerateCount + "\n");
		sb.Append (statusData.TotalTapPitCount + "\n");
		sb.Append (statusData.TotalCameEnemyCount + "\n");
		sb.Append (statusData.TotalAtackEnemyCount + "\n");
		sb.Append (statusData.TotalUsedSecomCount + "\n");
		sb.Append (statusData.TotalDamegedCount);
		statusParamLabel.text = sb.ToString ();
	}
}
