using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class StickyPlatform : MonoBehaviour {

	public List<Rigidbody> objectsOnPlatform = new List<Rigidbody>();
	public bool isMoving = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void moveTowards(Vector3 destination, float speed){

		foreach(Rigidbody body in objectsOnPlatform){
			body.transform.position = Vector3.MoveTowards(body.transform.position, destination, speed);
		}
		transform.position = Vector3.MoveTowards(transform.position, destination, speed);
	}



	void OnCollisionEnter(Collision collision){
		Debug.Log("Found contact on platform");
		if(collision.gameObject.rigidbody){ //Does this object have a rigid body?
			objectsOnPlatform.Add(collision.gameObject.rigidbody); //add it to list
		}
	}
	
	void OnCollisionExit(Collision collision){
		if(collision.gameObject.rigidbody){ //Does this object have a rigid body?
			objectsOnPlatform.Remove(collision.gameObject.rigidbody); //remove it from list
		}
	}

}
