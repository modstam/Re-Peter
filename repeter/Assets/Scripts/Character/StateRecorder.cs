using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateRecorder : MonoBehaviour {
	public float currentTime = 0.0f;
	public float lastRecordTime = 0.0f;
	public float recordingInterval = 0.5f;
	public float currentStates = 0.0f;

	public List<State> states = new List<State>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
			
		if((currentTime - lastRecordTime) > recordingInterval){
			FirstPersonCharacter controller = GetComponent<FirstPersonCharacter>();
			TransformData data = new TransformData();
			data.Clone (this.transform);
			states.Add(new State(currentTime, data, rigidbody.velocity, controller.jumpedThisFrame, controller.grounded));
			currentStates++;
			lastRecordTime = currentTime;
			//Debug.Log("State added");
		}

		currentTime += Time.deltaTime;
	}


	public List<State> getStates(){
		
		return this.states;
	}


	void resetTime(){
		currentTime = 0.0f;
		lastRecordTime = 0.0f;
	}


}





