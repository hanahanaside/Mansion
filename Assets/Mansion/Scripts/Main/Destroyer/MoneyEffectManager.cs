using UnityEngine;
using System.Collections;

public class MoneyEffectManager : MonoBehaviour {
	public ParticleSystem[] kiraArray;
	public ParticleSystem[] moneyArray;

	IEnumerator Start () {
		Init ();
		Transform trans = transform;
		for (int i = 0; i < trans.childCount; i++) {
			trans.GetChild (i).gameObject.renderer.material.renderQueue = 4000;
		}
		yield return new WaitForSeconds (1.0f);
		Destroy (gameObject);
	}

	void OnDisable () {
		Destroy (gameObject);
	}

	void Init () {
		foreach (ParticleSystem moneyParticle in moneyArray) {
			moneyParticle.startLifetime = Random.Range (0.3f, 0.5f);
			moneyParticle.startSpeed = Random.Range (1.5f, 2.0f);
			moneyParticle.startSize = Random.Range (0.05f, 0.1f);
			moneyParticle.startRotation = Random.Range (-30.0f, 90.0f);
			moneyParticle.gravityModifier = Random.Range (0.5f,1.0f);
		}
	}
}
