using UnityEngine;
using System.Collections;

public class AddCountLabelController : MonoBehaviour {

	void OnComplete(){
		Debug.Log("comp");
		Destroy(gameObject);
	}
}
