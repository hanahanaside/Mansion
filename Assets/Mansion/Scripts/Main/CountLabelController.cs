using UnityEngine;
using System.Collections;

public class CountLabelController : MonoBehaviour {

	public UILabel countLabel;

	void SetCount(int count){
		countLabel.text = count.ToString();
	}

	void OnComplete(){
		Destroy(gameObject);
	}
}
