using UnityEngine;
using System.Collections;

public class PlatformMover : MonoBehaviour {

	public Transform platform;
	public Transform origin;
	public Transform destination;
	public float speed = 0.1f; // movements per second
	bool moveToDest = false;
	
	void OnTriggerEnter(Collider other) {
		moveToDest = true;
	}
		
	void OnTriggerExit(Collider other) {
		moveToDest = false;
	}

	void FixedUpdate(){

		if(moveToDest){
			if(!(platform.transform.position == destination.transform.position)){
				platform.transform.position = Vector3.MoveTowards(platform.transform.position, destination.position, speed);
			}
		} else {
				if(!(platform.transform.position == origin.transform.position)){
					platform.transform.position = Vector3.MoveTowards(platform.transform.position, origin.position, speed);
			}
		}
	}
}

