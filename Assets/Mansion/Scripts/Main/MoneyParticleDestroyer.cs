using UnityEngine;
using System.Collections;

public class MoneyParticleDestroyer : MonoBehaviour {

	IEnumerator Start () {
		Transform trans = transform;
		for (int i = 0; i < trans.childCount; i++) {
			trans.GetChild (i).gameObject.renderer.material.renderQueue = 4000;
		}
		yield return new WaitForSeconds (2.0f);
		Destroy (gameObject);
	}
}
