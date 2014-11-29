using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformMover : MonoBehaviour {

	public Transform platform;
	public Transform origin;
	public Transform destination;
	public float speed = 0.1f; // movements per second
	bool moveToDest = false;

	public List<Rigidbody> objectsOnPlatform = new List<Rigidbody>();
	
	void OnTriggerEnter(Collider other) {
		moveToDest = true;
	}
		
	void OnTriggerExit(Collider other) {
		moveToDest = false;
	}

	void FixedUpdate(){

		if(moveToDest){

			if(!(platform.transform.position == destination.transform.position)){
				StickyPlatform stickyPlatform = platform.gameObject.GetComponent<StickyPlatform>();
				if(stickyPlatform){
					stickyPlatform.moveTowards(destination.position, speed);
				}
			}
		} else {
			if(!(platform.transform.position == origin.transform.position)){
				StickyPlatform stickyPlatform = platform.gameObject.GetComponent<StickyPlatform>();
				if(stickyPlatform){
					stickyPlatform.moveTowards(origin.position, speed);
				}
			}
		}
	}	
}

