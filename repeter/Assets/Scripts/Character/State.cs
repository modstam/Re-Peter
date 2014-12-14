using UnityEngine;
using System.Collections;

public class State {

	public TransformData stateTransform;
	public float stateTime;
	public Vector3 velocity = new Vector3();
	public bool jump;
	public bool grounded;
	public bool hitTrigger;
	public bool exitTrigger;

	public State(float time, TransformData transform, Vector3 velocity, bool jump, bool isGrounded, bool hitTrigger, bool exitTrigger){
		this.stateTime = time;
		this.stateTransform = transform;
		this.velocity.x = velocity.x;
		this.velocity.y = velocity.y;
		this.velocity.z = velocity.z;
		this.jump = jump;
		this.grounded = isGrounded;
		this.hitTrigger = hitTrigger;
		this.exitTrigger = exitTrigger;
	}
	
	public Vector3 getPosition(){
		return stateTransform.getPosition();
	}

	public Quaternion getRotationQuaternion(){
		return Quaternion.Euler (stateTransform.getRotation());
	}

	public Vector3 getRotationEuler(){
		return stateTransform.getRotation();
	}


}