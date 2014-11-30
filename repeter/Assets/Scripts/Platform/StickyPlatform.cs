using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class StickyPlatform : MonoBehaviour {

	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void moveTowards(Vector3 destination, float speed){
		transform.position = Vector3.MoveTowards(transform.position, destination, speed);
	}



	void OnCollisionEnter(Collision collision){
		if(collision.gameObject.rigidbody){ //Does this object have a rigid body?
			//Debug.Log("Rigidbody on platform");
			collision.gameObject.transform.parent = this.transform; //add this rigidbody as a child of this platform
		}
	}
	
	void OnCollisionExit(Collision collision){
		if(collision.gameObject.rigidbody){ //Does this object have a rigid body?
			collision.gameObject.transform.parent = null; //remove the rigidbody from this platform
		}
	}

}
