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
			Vector3 offset = this.transform.position - body.transform.position;
			body.transform.position = Vector3.MoveTowards(body.transform.position, destination, speed);

			//Vector3 newPos = (body.transform.position + offset);
			//body.transform.position = newPos;
		}

		transform.position = Vector3.MoveTowards(transform.position, destination, speed);


	}



	void OnCollisionEnter(Collision collision){

		if(collision.gameObject.rigidbody){ //Does this object have a rigid body?
			if(!objectsOnPlatform.Contains(collision.gameObject.rigidbody)){
				Debug.Log("Rigidbody on platform");
				objectsOnPlatform.Add(collision.gameObject.rigidbody); //add it to list
			}
	
		}
	}
	
	void OnCollisionExit(Collision collision){
		if(collision.gameObject.rigidbody){ //Does this object have a rigid body?
			objectsOnPlatform.Remove(collision.gameObject.rigidbody); //remove it from list
		}
	}

}
