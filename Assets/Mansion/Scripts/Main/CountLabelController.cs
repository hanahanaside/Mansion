using UnityEngine;
using System.Collections;

public class CountLabelController : MonoBehaviour {

	public UILabel countLabel;

	void OnDisable(){
		Destroy(gameObject);
	}

	void SetCount(string count){
		countLabel.text = count;
	}

	void OnComplete(){
		Destroy(gameObject);
	}
}
