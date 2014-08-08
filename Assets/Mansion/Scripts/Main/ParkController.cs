using UnityEngine;
using System.Collections;

public class ParkController : MonoBehaviour {

	public GameObject addCountLabelPrefab;
	public GameObject moneyParticlePrefab;
	public GameObject hole;

	public void OnHoleClicked () {
		InstantiateObject(addCountLabelPrefab);
		InstantiateObject(moneyParticlePrefab);
		CountManager.Instance.AddGeneratedCount(10);
	}

	private void InstantiateObject(GameObject prefab){
		GameObject instantiateObject = Instantiate (prefab)as GameObject;
		instantiateObject.transform.parent = transform.parent;
		instantiateObject.transform.localPosition = hole.transform.localPosition;
		instantiateObject.transform.localScale = new Vector3 (1, 1, 1);
	}
}
