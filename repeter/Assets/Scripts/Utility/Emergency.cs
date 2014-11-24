using UnityEngine;
using System.Collections;

public class Emergency : MonoBehaviour {

	// audio clip references
	[SerializeField] AudioClip shutDown;		// The audio for lights shuting down
	[SerializeField] AudioClip powerUp;			// The audio for power back up

	// Use this for initialization
	IEnumerator Start () {
		yield return new WaitForSeconds(1);
		foreach (Transform child in transform) {
			Light l = child.gameObject.GetComponentInChildren<Light>();
			child.gameObject.audio.clip = shutDown;
			child.gameObject.audio.Play();
			StartCoroutine(FadeToBlack(l));
			yield return new WaitForSeconds(Random.value);
		}
		yield return new WaitForSeconds(2);
		foreach (Transform child in transform) {
			child.gameObject.audio.clip = powerUp;
			child.gameObject.audio.volume = 0.3f;
			child.gameObject.audio.Play();
			Light l = child.gameObject.GetComponentInChildren<Light>();
			l.color  = Color.red;
			l.intensity = .5f;
		}
		 	
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	IEnumerator FadeToBlack(Light l){
		for (int i = 0; i<100; i++) {
			l.color -= Color.white / 2.0F * i;
		}
		yield return new WaitForSeconds(0.1f);
	}
}
