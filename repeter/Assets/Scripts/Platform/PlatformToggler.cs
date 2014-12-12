using UnityEngine;
using System.Collections;

public class PlatformToggler : MonoBehaviour {

	public Transform platform;
	public bool showWhenOnTrigger;
	private bool trig;
	
	
	void OnTriggerEnter(Collider other) {
		trig = true;
	}
	
	void OnTriggerExit(Collider other) {
		trig = false;
	}
	
	void FixedUpdate(){
		platform.gameObject.SetActive(!(trig ^ showWhenOnTrigger));
	}
}
