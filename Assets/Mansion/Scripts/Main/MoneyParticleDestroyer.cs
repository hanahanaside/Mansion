using UnityEngine;
using System.Collections;

public class MoneyParticleDestroyer : MonoBehaviour {

	IEnumerator Start () {
		renderer.material.renderQueue = 4000;
		yield return new WaitForSeconds (2.0f);
		Destroy (gameObject);
	}
}
