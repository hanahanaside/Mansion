using UnityEngine;
using System.Collections;

public class ParkController : MonoBehaviour {

	public GameObject addCountLabelPrefab;
	public GameObject hole;

	public void OnHoleClicked () {
		GameObject addCountLabelObject = Instantiate (addCountLabelPrefab)as GameObject;
		addCountLabelObject.transform.parent = transform.parent;
		addCountLabelObject.transform.localPosition = hole.transform.localPosition;
		addCountLabelObject.transform.localScale = new Vector3 (1, 1, 1);
		CountManager.Instance.AddCount(10);
	}
}
