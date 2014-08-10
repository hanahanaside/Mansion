using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

	public GameObject particle;
	public GameObject uiRoot;

	// Use this for initialization
	IEnumerator Start () {
		GameObject particleObj =  Instantiate(particle) as GameObject;
		particleObj.transform.parent = uiRoot.transform;
		particleObj.transform.localScale = new Vector3(1,1,1);
		yield return new WaitForSeconds(2.0f);

		StartCoroutine(Start());
	}
	
	void OnApplicationPause(bool state){
		Debug.Log("paaa");
	}
}
