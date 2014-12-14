using UnityEngine;
using System.Collections;

public class SimplePlatformToggler : MonoBehaviour {

	public Transform platformOn;
	public Transform platformOff;
	private bool trig;

	void OnTriggerEnter(Collider other) {
		trig = true;
	}
	
	void OnTriggerExit(Collider other) {
		trig = false;
	}
	
	void FixedUpdate(){
		platformOn.gameObject.SetActive(trig);
		platformOff.gameObject.SetActive(!trig);
	}
}
